﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RemindEdit.aspx.cs" Inherits="WE_Project.Web.Message.RemindEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>修改提示信息</title>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <script type="text/javascript" charset="utf-8" src="/plugin/UEditor/editor_config.js"></script>
    <script type="text/javascript" charset="utf-8" src="/plugin/UEditor/editor_all.js"></script>
</head>
<body>
    <div id="mempay">
        <div id="finance">
            <form id="form1">
            <input type="hidden" runat="server"  id="hidId"/>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td width="15%" align="right">
                        类型分类<input runat="server" id="lbID" type="hidden" />
                    </td>
                    <td width="75%" style="height: 40px;">
                        <input id="txtNTitle" class="normal_input" runat="server" style="width: 50%;" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        内容
                    </td>
                    <td style="padding: 15px;">
                        <script id="editor" type="text/plain"><%=txtNContent %></script>
                        <input name="hdContent" id="hdContent" type="hidden" />
                    </td>
                </tr>
                <tr>
                    <td width="15%" align="right">
                   
                    </td>
                    <td width="75%" align="left">
                         <input type="reset" class="normal_btnok" value="重 置" style="float: left;" />&nbsp;
                        <input type="button" class="normal_btnok" value="发 布" style="float: left;" onclick="checkChange();" />
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
    <script type="text/javascript">
        var ue = UE.getEditor('editor');

        function checkChange() {
            if ($('#txtNTitle').val() == '') {
                v5.error('类型分类不能为空', '1', 'ture');
            } else if (ue.getContent() == '') {
                v5.error('内容不能为空', '1', 'ture');
            } else {
                $('#hdContent').val(encodeURI(ue.getContent()));
                ActionModel("/Message/RemindEdit.aspx?Action=Modify", $('#form1').serialize());
            }
        }
    </script>
</body>
</html>
