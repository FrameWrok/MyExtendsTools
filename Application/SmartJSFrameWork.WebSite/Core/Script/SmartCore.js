function init() {
    var arrScript = document.getElementsByTagName("script");
    var index = -1;
    for (var i = 0; i < arrScript.length; i++) {
        if (arrScript[i].src.indexOf("/SmartBase.js") != -1) {
            index = arrScript[i].src.indexOf("/SmartBase.js");
            break;
        }
    }
    if (index != -1)
        return;
    for (var i = 0; i < arrScript.length; i++) {
        if (arrScript[i].src.indexOf("/SmartCore.js") != -1) {
            var path = arrScript[i].src.substr(0, arrScript[i].src.indexOf("/SmartCore.js") + 1) + "SmartBase.js";
            var html = "<script language=\"javascript\" type=\"text/javascript\" src=\"" + path + "\"><\/script>";
            document.write(html);
            break;
        }
    }
}
init();
//var Core = new Class("Core");
