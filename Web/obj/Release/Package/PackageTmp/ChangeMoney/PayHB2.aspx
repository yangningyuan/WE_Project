﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayHB2.aspx.cs" Inherits="WE_Project.Web.ChangeMoney.PayHB2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        #finance table tr {
            vertical-align: middle;
            text-align: center;
            height: 50px;
        }

            #finance table tr td {
                padding: 0px;
                vertical-align: middle;
            }

        .bank table tr td img {
            width: 154px;
        }

        .recharge {
            background: #FD6F0D;
            background: rgba(253, 111, 13, 0.35);
            padding: 20px 0px;
            color: white;
            font-size: 24px;
            text-align: center;
            margin: 20px 0px;
        }

            .recharge b {
                margin: 0px 20px;
            }

        .sel {
            display: inline-block;
        }
    </style>
    <script type="text/javascript">
      

        //function checkJe() {
        //    if ($('#txtValidMoney').val().Trim() == "") {
        //        v5.error('充值金额不能为空', '1', 'true');
        //        return false;
        //    } else {
        //        return true;
        //    }
        //}

        function Redirect() {
            if ($('#txtValidMoney').val().Trim() == "") {
                v5.error('充值金额不能为空', '1', 'true');
            } else {
                //var ss = $("input:radio:checked").val();

                document.forms[0].action = "Payment/cai1pay/redirect.aspx";

                document.forms[0].submit();
            }
        }
    </script>
</head>

<body>
    <div id="mempay">
        <div id="finance" class="bank">
            <%--<input type="hidden" id="bankauto"  runat="server" />--%>
            <form id="form1" method="get" target="_blank" action="Payment/cai1pay/redirect.aspx">
                <input type="hidden" id="tmid" name="tmid" runat="server" />
                <span class="remak">温馨提示：请在新打开的页面中完成支付</span>
                <div class="recharge">
                    <b>请选择支付方式</b><br />
                     <span style="color:#ffd800; font-size:18px; font-weight:bold;"><%=WE_Project.BLL.Reward.List["MGP"].RewardName %>最低支付100元且是100元的倍数</span><br />
                            <span style="color:#ffd800; font-size:18px; font-weight:bold;"><%=WE_Project.BLL.Reward.List["TotalYFHMoney"].RewardName %>最低支付200元且是200的倍数</span>
                </div>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="text-align: right; font-size: 24px;">
                            <span style="font-size: 2rem;">充值类型：</span>
                           
                        </td>
                        <td colspan="3" style="padding-left: 20px; text-align: left;">
                            <input type="radio" value="1" name="CZType" checked="checked"><%=WE_Project.BLL.Reward.List["MGP"].RewardName %>
                            <input type="radio" value="2" name="CZType"><%=WE_Project.BLL.Reward.List["TotalYFHMoney"].RewardName %>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; font-size: 24px;">
                            <span style="font-size: 2rem;">充值金额：</span>
                        </td>
                        <td colspan="3" style="padding-left: 20px; text-align: left;">
                            <input id="txtValidMoney" name="txtValidMoney" class="normal_input" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input name="yh" type="radio" value="01050000">
                            <img src="../Payment/banks/jianshe.gif">
                        </td>
                        <td>
                            <input name="yh" type="radio" value="01030000">
                            <img src="../Payment/banks/nongye.gif">
                        </td>
                        <td>
                            <input name="yh" type="radio" value="01020000" checked="checked">
                            <img src="../Payment/banks/gongshang.gif">
                        </td>
                        <td>
                            <input name="yh" type="radio" value="03080000">
                            <img src="../Payment/banks/zhaohang.gif">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input name="yh" type="radio" value="03010000">
                            <img src="../Payment/banks/jiaotong.gif">
                        </td>
                        <td>
                            <input name="yh" type="radio" value="04030000">
                            <img src="../Payment/banks/youzheng.gif">
                        </td>
                        <td>
                            <input name="yh" type="radio" value="01040000">
                            <img src="../Payment/banks/zhongguo.gif">
                        </td>
                        <td>
                            <input name="yh" type="radio" value="03030000">
                            <img src="../Payment/banks/guangda.gif">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input name="yh" type="radio" value="03020000">
                            <img src="../Payment/banks/zhongxin.gif">
                        </td>
                        <td>
                            <input name="yh" type="radio" value="03060000">
                            <img src="../Payment/banks/guangfa.gif">
                        </td>
                        <td>
                            <input name="yh" type="radio" value="03100000">
                            <img src="../Payment/banks/shangpufa.gif">
                        </td>
                        <td>
                            <input name="yh" type="radio" value="03130011">
                            <img src="../Payment/banks/beijing.gif">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input name="yh" type="radio" value="03040000">
                            <img src="../Payment/banks/huaxia.gif">
                        </td>
                        <td>
                            <input name="yh" type="radio" value="03090000">
                            <img src="../Payment/banks/cib.gif">
                        </td>
                        <td>
                            <input name="yh" type="radio" value="03050000">
                            <img src="../Payment/banks/minsheng.gif">
                        </td>
                        <td>
                            <input name="yh" type="radio" value="03070000">
                            <img src="../Payment/banks/pingan.gif">
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3" align="center" style="padding-left: 20px; text-align: left;">
                            <input type="button" name="Submit" value="确定" id="Submit2" class="normal_btnok" onclick="Redirect()" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
</body>
</html>
