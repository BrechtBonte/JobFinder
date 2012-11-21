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

    private static bool ActivationExists(string code) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Loggers.SingleOrDefault(l => l.Activation == code) != null;
    }

    public static bool MailExists(string email) {

        DataClassesDataContext dbo = new DataClassesDataContext();

        return dbo.Loggers.SingleOrDefault(l => l.Email == email) != null;
    }

    public static Logger CreateUser(string email, string password, string imageName, string firstname, string lastname, string mail, string telephone, string cv, string description, bool showMail) {

        KeyValuePair<string, string> pass;
        do {
            pass = Encrypt(password);
        } while (SaltExists(pass.Key));

        DataClassesDataContext dbo = new DataClassesDataContext();

        User usr = new User();
        usr.ImageName = imageName;
        usr.Firstname = firstname;
        usr.Lastname = lastname;
        if(mail != null && mail != "") usr.Email = mail.ToString();
        if(telephone != null && telephone != "") usr.Telephone = telephone.ToString();
        usr.Cv = cv;
        if(description != null && description != "") usr.Description = description.ToString();

        dbo.Users.InsertOnSubmit(usr);

        dbo.SubmitChanges();

        Logger log = new Logger();
        log.Email = email;
        log.Salt = pass.Key;
        log.Password = pass.Value;
        do { log.Activation = GetActivationString(); } while (ActivationExists(log.Activation));
        log.Activated = true;
        log.UserId = usr.ID;
        log.LastLogin = DateTime.Now;
        log.LoginBuff = DateTime.Now;

        dbo.Loggers.InsertOnSubmit(log);

        dbo.SubmitChanges();

        return log;
    }

    public static Logger CreateCompany(string email, string password, string name, string description, string logo, string website, string street, string city, int region) {

        KeyValuePair<string, string> pass;
        do {
            pass = Encrypt(password);
        } while (SaltExists(pass.Key));

        DataClassesDataContext dbo = new DataClassesDataContext();

        Company comp = new Company();
        comp.Name = name;
        comp.Description = description;
        comp.Logo = logo;
        comp.Website = website;
        comp.Street = street;
        comp.City = city;
        comp.RegionId = region;

        dbo.Companies.InsertOnSubmit(comp);

        dbo.SubmitChanges();

        Logger log = new Logger();
        log.Email = email;
        log.Salt = pass.Key;
        log.Password = pass.Value;
        do { log.Activation = GetActivationString(); } while (ActivationExists(log.Activation));
        log.Activated = true;
        log.CompanyId = comp.ID;
        log.LastLogin = DateTime.Now;
        log.LoginBuff = DateTime.Now;

        dbo.Loggers.InsertOnSubmit(log);

        dbo.SubmitChanges();

        return log;
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

    public void LoggedIn() {

        DataClassesDataContext dbo = new DataClassesDataContext();

        this.LastLogin = this.LoginBuff;
        this.LoginBuff = DateTime.Now;

        dbo.SubmitChanges();
    }

    #endregion


    #region - private -

    private static Random random = new Random((int)DateTime.Now.Ticks);
    private const int ACTIVATION_CHARS = 20;
    private static string  GetActivationString() {
        StringBuilder builder = new StringBuilder();
        char ch;
        for (int i = 0; i < ACTIVATION_CHARS; i++) {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));                 
            builder.Append(ch);
        }

        return builder.ToString();
    }

    private static KeyValuePair<string, string> Encrypt(string password) {

        RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
        byte[] salt = new byte[SALT_BYTES];
        csprng.GetBytes(salt);

        string saltStr = Convert.ToBase64String(salt);

        return new KeyValuePair<string, string>(saltStr, Encrypt(saltStr, password));
    }

    private static string Encrypt(string salt, string password) {

        Rfc2898DeriveBytes passKey = new Rfc2898DeriveBytes(password, Encoding.ASCII.GetBytes(salt), ITERATIONS);
        byte[] hash = passKey.GetBytes(HASH_Bytes);

        return Convert.ToBase64String(hash);
    }

    #endregion
}