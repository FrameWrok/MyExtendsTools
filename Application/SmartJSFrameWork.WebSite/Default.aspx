<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SmartJSFrameWork.WebSite._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="crypto/Barrett.js"></script>
    <script type="text/javascript" src="crypto/BigInt.js"></script>
    <script type="text/javascript" src="crypto/RSA.js"></script>
    <script type="text/javascript" src="crypto/Base64Tool.js"></script>
    <script language="javascript" type="text/javascript">

        function funcEncrypt(publicKey, content)
        {
            publicKey = document.getElementById("publicKey").value;
            //publicKey = "e01vZHVsdXM6J0M2RTVGREFFNDY1RTg4NzVFMzk1MjVCQjVEREE0QzlGQkYzN0M0Q0Q3RkQxQkVGQUM3MzAwNThEMDQzMDczRTNFRDY2QjU4M0QyNTkxMTNGNDI5MkI3QkY3OTlGQzFCMTNCQzY4QkI0NjVBRjI5NkMyMjUyRUM3MDU4OEFDMzc3RDI1RkZERDNEODIxODY2ODA5RUE5MTEwMzQ5RTJBNDAzOUEwRDQ5OTIyNTFGNEM3NzY3MDQ5M0Y0RjU1QjhDOUE2MUJDOTc1RkFEMTc2RDk4MEVDNUI2MDQwQUQ1NTEyOTAxODBDOTU0NTlFMjBCMzkwRTYxMUJGNkRFQTUxNjMnLEV4cG9uZW50OicwMTAwMDEnfQ==";
            content = "lbl123|lbl123";
            var publicKeyJsonString = base64decode(publicKey);
            var jsonobject = eval("(" + publicKeyJsonString + ")");
            setMaxDigits(131);
            var key = new RSAKeyPair(jsonobject.Exponent, "", jsonobject.Modulus);
            document.getElementById("encryptPwd").value = encryptedString(key, base64encode(content));
            //return encryptedString(key, base64encode(content));
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:RadioButton runat="server" ID="radio1" Text="选择一" GroupName="a" />
        <asp:RadioButton runat="server" ID="radio2" Text="选择二" GroupName="a" />
        <textarea id="publicKey" rows="2" cols="100"></textarea>
        <input type="button" onclick="funcEncrypt()" value="RSA测试" />
        <textarea id="encryptPwd" rows="10" cols="100"></textarea>
    </div>
    </form>
</body>
</html>
