function loadevent() {

    if (pageLoad) pageLoad();

    var first = document.getElementsByClassName("firstInput")[0];
    if (first) first.focus();

    var link = document.getElementsByClassName("loggerLinks")[0];

    if (link) {

        var width = link.getElementsByTagName("p")[0].offsetWidth;

        var lis = document.getElementsByClassName("dropdown")[0].getElementsByTagName("a");
        for (var i = 0; i < lis.length; i++) {
            lis[i].style.width = (width - 10) + "px";
        }

    }

    document.onclick = function (e) {

        var d = e.target;

        while (d != null && d.className != "loggerLinks clearfix opened" && d.className != "loggerLinks clearfix" && d.className != "dropdownArrow") {
            d = d.parentNode;
        }

        if (d != null && link.className == "loggerLinks clearfix") {
            link.className = "loggerLinks clearfix opened";
            return;
        }

        if ((d == null || d.className == "dropdownArrow" || d.className == "loggerLinks clearfix opened") && link.className == "loggerLinks clearfix opened") {
            link.className = "loggerLinks clearfix";
        }
    }
}

if (document) window.onload = loadevent;