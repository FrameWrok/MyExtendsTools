<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginNew.aspx.cs" Inherits="SmartJSFrameWork.WebSite.LoginNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="crypto/Barrett.js"></script>
    <script type="text/javascript" src="crypto/BigInt.js"></script>
    <script type="text/javascript" src="crypto/RSA.js"></script>
    <script language="javascript" type="text/javascript">


        function cmdEncrypt()
        {
            setMaxDigits(131);
            <%--var key = new RSAKeyPair("<%=GetRSA_E()%>", "", "<%=GetRSA_M()%>");--%>
            document.getElementById("posx").value = encryptedString(key, base64encode(document.getElementById("txtUserName").value) + "\\" + base64encode(document.getElementById("txtPassword").value));
            document.getElementById("txtPassword").value = "";
            return;
        }
        function base64encode(str)
        {

            var base64EncodeChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
            var base64DecodeChars = new Array(
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 62, -1, -1, -1, 63,
			52, 53, 54, 55, 56, 57, 58, 59, 60, 61, -1, -1, -1, -1, -1, -1,
			-1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14,
			15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, -1, -1, -1, -1, -1,
			-1, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
			41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, -1, -1, -1, -1, -1);
            var out, i, len;
            var c1, c2, c3;

            len = str.length;
            i = 0;
            out = "";
            while (i < len)
            {
                c1 = str.charCodeAt(i++) & 0xff;
                if (i == len)
                {
                    out += base64EncodeChars.charAt(c1 >> 2);
                    out += base64EncodeChars.charAt((c1 & 0x3) << 4);
                    out += "==";
                    break;
                }
                c2 = str.charCodeAt(i++);
                if (i == len)
                {
                    out += base64EncodeChars.charAt(c1 >> 2);
                    out += base64EncodeChars.charAt(((c1 & 0x3) << 4) | ((c2 & 0xF0) >> 4));
                    out += base64EncodeChars.charAt((c2 & 0xF) << 2);
                    out += "=";
                    break;
                }
                c3 = str.charCodeAt(i++);
                out += base64EncodeChars.charAt(c1 >> 2);
                out += base64EncodeChars.charAt(((c1 & 0x3) << 4) | ((c2 & 0xF0) >> 4));
                out += base64EncodeChars.charAt(((c2 & 0xF) << 2) | ((c3 & 0xC0) >> 6));
                out += base64EncodeChars.charAt(c3 & 0x3F);
            }
            return out;
        }

        var IsLoginStarted = false;

        function startTimer()
        {
            var label = document.getElementById('lblMsg');
            label.innerHTML = '<b>Please wait : </b>';

            window.setTimeout('showProgress()', 250);
        }

        function showProgress(n)
        {
            if (IsLoginStarted)
            {
                var label = document.getElementById('lblMsg');
                label.innerHTML += '<b>|</b>';

                window.setTimeout('showProgress()', 250);
            }
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right" height="28">
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="3" cellspacing="0" style="background-color: white">
        <tr>
            <td>
                <span>Log On to APPa</span>
                <br>
                <hr size="1">
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="3" cellspacing="0" border="0">
        <tr>
            <td>
                <br />
                Please enter your user name and password:<br />
                <hr size="1" />
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="1">
                    <tr>
                        <td>
                            User Name:
                        </td>
                        <td width="20">
                            &nbsp;
                        </td>
                        <td>
                            <asp:TextBox EnableViewState="False" MaxLength="30" ID="txtUserName" runat="server"
                                Width="200px"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Password:
                        </td>
                        <td width="20">
                            &nbsp;
                        </td>
                        <td>
                            <asp:TextBox EnableViewState="False" MaxLength="30" ID="txtPassword" runat="server"
                                TextMode="Password" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                        <td align="left">
                            <table cellpadding="0" border="0">
                                <tr>
                                    <td class="">
                                        <div class="">
                                            <input type="button" id="btnLogin" runat="Server" value="Login" onclick="javascript:cmdEncrypt();"
                                                name="btnLogin" onserverclick="btnLogin_ServerClick" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblMsg" EnableViewState="False" runat="server"></asp:Label><br/>
                <asp:Label ID="lblError" EnableViewState="False" runat="server" ForeColor="red"></asp:Label><br/>
                <asp:HiddenField ID="posx" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
