using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// Summary description for Logger
/// </summary>
public partial class Logger {

    private const int SALT_BYTES = 24;
    private const int HASH_Bytes = 24;
    private const int ITERATIONS = 1000;

    #region - General -

    public static Logger GetLogger(int id) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Loggers.SingleOrDefault(l => l.ID == id);
    }

    public static Logger GetLogger(string email) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Loggers.SingleOrDefault(l => l.Email == email);
    }

    public static Logger GetLogger(object id) {
        return GetLogger(Convert.ToInt32(id));
    }

    public static bool Exists(string email) {

        return GetLogger(email) != null;
    }

    public static bool Exists(int id) {

        return GetLogger(id) != null;
    }

    private static bool SaltExists(string salt) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Loggers.SingleOrDefault(l => l.Salt == salt) != null;
    }

    #endregion


    #region - Instance -

    public bool IsUser { get { return this.UserId != null; } }

    public bool IsCompany { get { return this.CompanyId != null; } }

    public string Name {
        get {
            if (IsUser) return this.User.Firstname + " " + this.User.Lastname;
            else return this.Company.Name;
        }
    }

    public bool CheckPassword(string pass) {

        return this.Password == Encrypt(this.Salt, pass);
    }

    #endregion

    #region - private -

    private KeyValuePair<string, string> Encrypt(string password) {

        RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
        byte[] salt = new byte[SALT_BYTES];
        csprng.GetBytes(salt);

        string saltStr = Convert.ToBase64String(salt);

        return new KeyValuePair<string, string>(saltStr, Encrypt(saltStr, password));
    }

    private string Encrypt(string salt, string password) {

        Rfc2898DeriveBytes passKey = new Rfc2898DeriveBytes(password, Encoding.ASCII.GetBytes(salt), ITERATIONS);
        byte[] hash = passKey.GetBytes(HASH_Bytes);

        return Convert.ToBase64String(hash);
    }

    #endregion
}