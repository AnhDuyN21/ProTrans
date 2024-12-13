using Application.Interfaces.InterfaceRepositories;
using Application.ViewModels.SendMail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static QuestPDF.Helpers.Colors;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Infrastructures.Repositories
{
    public class SendMailRepository : ISendMailRepository
    {
        private readonly SmtpClient _smtpClient;

        public SendMailRepository(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        public async Task SendEmailAsync(MessageDTO message)
        {
            var mailMessage = new MailMessage("teptai48@gmail.com", message.To)
            {
                Subject = message.Subject,
                Body = $"<html><body><p>{message.Body}</p><img src='{message.ImageUrl}'/></body></html>",
                IsBodyHtml = true
            };

            await _smtpClient.SendMailAsync(mailMessage);
        }
        public byte[] CreatePDF(Domain.Entities.Order order, Domain.Entities.Document[] documents, Domain.Entities.DocumentPrice[] prices, string ShipperName, string shipperPhone, bool? pickupRequest)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var document = Document.Create(container =>
            {
                container.Page(p =>
                {
                    p.Size(PageSizes.A4);
                    p.Margin(1, Unit.Centimetre);
                    p.PageColor(Colors.White);
                    p.DefaultTextStyle(x => x.FontSize(20).FontFamily("Arial"));

                    p.Content()
                    .Table(
                        contract =>
                        {
                            contract.ColumnsDefinition(cols =>
                            {
                                cols.RelativeColumn();
                            });
                            contract.Cell().BorderVertical(1).BorderTop(1).BorderBottom(1).PaddingVertical(1).Column(x =>
                            {
                                x.Item().Row(header =>
                                {
                                    header.RelativeItem().Column(col =>
                                    {
                                        col.Item().AlignCenter().Text("Bill").FontSize(16).Italic().FontColor(Colors.Black);

                                        col.Item().AlignCenter().Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(12).FontColor(Colors.Black));
                                            text.Span("Date:");
                                            text.Span($" {DateTime.Now.Day}").SemiBold();
                                            text.Span(" month");
                                            text.Span($" {DateTime.Now.Month}").SemiBold();
                                            text.Span(" year");
                                            text.Span($" {DateTime.Now.Year}").SemiBold();
                                        });

                                    });
                                }
                                );
                            });


                            contract.Cell().BorderVertical(1).BorderBottom(1).PaddingHorizontal(5).PaddingVertical(10).Column(
                                col =>
                                {
                                    col.Item().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12).FontColor(Colors.Black));
                                        text.Span("Company Name: ");
                                        text.Span("ProTrans - Dịch vụ dịch thuật chuyên nghiệp").SemiBold();
                                    });
                                    col.Item().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12).FontColor(Colors.Black));
                                        text.Span("Address: ");
                                        text.Span("Thu Duc district, Ho Chi Minh city");
                                    });
                                    col.Item().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12).FontColor(Colors.Black));
                                        text.Span("Phone: ");
                                        text.Span("0123456789");
                                    });
                                });
                            contract.Cell().BorderVertical(1).BorderBottom(1).PaddingHorizontal(5).PaddingVertical(10).Column(
                                col =>
                                {
                                    col.Item().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12).FontColor(Colors.Black));
                                        text.Span("Họ tên khách hàng: ");
                                        text.Span(order.FullName);
                                    });
                                    col.Item().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12).FontColor(Colors.Black));
                                        text.Span("Số điện thoại khách hàng: ");
                                        text.Span(order.PhoneNumber);
                                    });
                                    col.Item().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12).FontColor(Colors.Black));
                                        text.Span("Địa chỉ khách hàng: ");
                                        text.Span(order.Address);
                                    });
                                });
                            contract.Cell().BorderVertical(1).BorderBottom(1).PaddingHorizontal(5).PaddingVertical(10).Column(
                                      col =>
                                      {
                                          col.Item().Text(text =>
                                          {
                                              text.DefaultTextStyle(x => x.FontSize(12).FontColor(Colors.Black));
                                              text.Span("Tên người giao đơn: ");
                                              text.Span(ShipperName);
                                          });
                                          col.Item().Text(text =>
                                          {
                                              text.DefaultTextStyle(x => x.FontSize(12).FontColor(Colors.Black));
                                              text.Span("Số điện thoại người giao đơn :");
                                              text.Span(shipperPhone);
                                          });
                                      });
                            contract.Cell().PaddingVertical(10).Table(
                            table =>
                            {
                                var total = order.TotalPrice.Value.ToString("N0");
                                table.ColumnsDefinition(col =>
                                {
                                    col.RelativeColumn();
                                    col.RelativeColumn();
                                });
                                table.Header(header =>
                                {
                                    header.Cell().Text("Mã tài liệu").FontSize(12).FontColor(Colors.Black).AlignCenter();
                                    header.Cell().Text("Đơn giá").FontSize(12).FontColor(Colors.Black).AlignCenter();
                                });
                                for (int i = 0; i < documents.Length; i++)
                                {
                                    table.Cell().Border(1).PaddingHorizontal(4).PaddingVertical(2).AlignMiddle().AlignRight().Text(documents[i].Code).FontSize(12).FontColor(Colors.Black);
                                    table.Cell().Border(1).PaddingHorizontal(4).PaddingVertical(2).AlignMiddle().AlignRight().Text(prices[i].Price.Value.ToString("N0")).FontSize(12).FontColor(Colors.Black);
                                }
                            });
                            contract.Cell().PaddingVertical(10).Table(
                                table =>
                                {
                                    var total = order.TotalPrice.Value.ToString("N0");
                                    table.ColumnsDefinition(col =>
                                    {
                                        col.RelativeColumn();
                                        col.RelativeColumn();
                                    });
                                    if (pickupRequest == true)
                                    {
                                        table.Cell().Border(1).PaddingHorizontal(4).PaddingVertical(2).AlignMiddle().AlignRight().Text("Phí nhận tài liệu:").FontSize(12).FontColor(Colors.Black);
                                        table.Cell().Border(1).PaddingHorizontal(4).PaddingVertical(2).AlignMiddle().AlignRight().Text("30,000").FontSize(12).FontColor(Colors.Black);
                                    }
                                    if (order.ShipRequest)
                                    {
                                        table.Cell().Border(1).PaddingHorizontal(4).PaddingVertical(2).AlignMiddle().AlignRight().Text("Phí vận chuyển:").FontSize(12).FontColor(Colors.Black);
                                        table.Cell().Border(1).PaddingHorizontal(4).PaddingVertical(2).AlignMiddle().AlignRight().Text("30,000").FontSize(12).FontColor(Colors.Black);
                                    }

                                    table.Cell().Border(1).PaddingHorizontal(4).PaddingVertical(2).AlignMiddle().AlignRight().Text("Tổng giá trị đơn hàng:").FontSize(12).FontColor(Colors.Black);
                                    table.Cell().Border(1).PaddingHorizontal(4).PaddingVertical(2).AlignMiddle().AlignRight().Text($"{total}").FontSize(12).FontColor(Colors.Black);

                                    ;
                                });
                        });
                });
            });
            using var stream = new MemoryStream();
            document.GeneratePdf(stream);
            return stream.ToArray();

        }
        public async Task SendEmailWithPDFAsync(MessageDTO message, byte[] pdf, string imageURL, string customerName)
        {
            var mailMessage = new MailMessage("teptai48@gmail.com", message.To)
            {
                Subject = message.Subject,
                Body = "<!--\r\n* This email was built using Tabular.\r\n* For more information, visit https://tabular.email\r\n-->\r\n<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" lang=\"en\">\r\n<head>\r\n<title></title>\r\n<meta charset=\"UTF-8\" />\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />\r\n<!--[if !mso]>-->\r\n<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />\r\n<!--<![endif]-->\r\n<meta name=\"x-apple-disable-message-reformatting\" content=\"\" />\r\n<meta content=\"target-densitydpi=device-dpi\" name=\"viewport\" />\r\n<meta content=\"true\" name=\"HandheldFriendly\" />\r\n<meta content=\"width=device-width\" name=\"viewport\" />\r\n<meta name=\"format-detection\" content=\"telephone=no, date=no, address=no, email=no, url=no\" />\r\n<style type=\"text/css\">\r\ntable {\r\nborder-collapse: separate;\r\ntable-layout: fixed;\r\nmso-table-lspace: 0pt;\r\nmso-table-rspace: 0pt\r\n}\r\ntable td {\r\nborder-collapse: collapse\r\n}\r\n.ExternalClass {\r\nwidth: 100%\r\n}\r\n.ExternalClass,\r\n.ExternalClass p,\r\n.ExternalClass span,\r\n.ExternalClass font,\r\n.ExternalClass td,\r\n.ExternalClass div {\r\nline-height: 100%\r\n}\r\nbody, a, li, p, h1, h2, h3 {\r\n-ms-text-size-adjust: 100%;\r\n-webkit-text-size-adjust: 100%;\r\n}\r\nhtml {\r\n-webkit-text-size-adjust: none !important\r\n}\r\nbody, #innerTable {\r\n-webkit-font-smoothing: antialiased;\r\n-moz-osx-font-smoothing: grayscale\r\n}\r\n#innerTable img+div {\r\ndisplay: none;\r\ndisplay: none !important\r\n}\r\nimg {\r\nMargin: 0;\r\npadding: 0;\r\n-ms-interpolation-mode: bicubic\r\n}\r\nh1, h2, h3, p, a {\r\nline-height: inherit;\r\noverflow-wrap: normal;\r\nwhite-space: normal;\r\nword-break: break-word\r\n}\r\na {\r\ntext-decoration: none\r\n}\r\nh1, h2, h3, p {\r\nmin-width: 100%!important;\r\nwidth: 100%!important;\r\nmax-width: 100%!important;\r\ndisplay: inline-block!important;\r\nborder: 0;\r\npadding: 0;\r\nmargin: 0\r\n}\r\na[x-apple-data-detectors] {\r\ncolor: inherit !important;\r\ntext-decoration: none !important;\r\nfont-size: inherit !important;\r\nfont-family: inherit !important;\r\nfont-weight: inherit !important;\r\nline-height: inherit !important\r\n}\r\nu + #body a {\r\ncolor: inherit;\r\ntext-decoration: none;\r\nfont-size: inherit;\r\nfont-family: inherit;\r\nfont-weight: inherit;\r\nline-height: inherit;\r\n}\r\na[href^=\"mailto\"],\r\na[href^=\"tel\"],\r\na[href^=\"sms\"] {\r\ncolor: inherit;\r\ntext-decoration: none\r\n}\r\n</style>\r\n<style type=\"text/css\">\r\n@media (min-width: 481px) {\r\n.hd { display: none!important }\r\n}\r\n</style>\r\n<style type=\"text/css\">\r\n@media (max-width: 480px) {\r\n.hm { display: none!important }\r\n}\r\n</style>\r\n<style type=\"text/css\">\r\n@media (max-width: 480px) {\r\n.t57{padding:0 0 22px!important}.t14,.t48,.t59,.t75{width:480px!important}.t42,.t53,.t69,.t8{text-align:center!important}.t41,.t52,.t68,.t7{vertical-align:top!important;width:600px!important}.t5{border-top-left-radius:0!important;border-top-right-radius:0!important;padding:20px 30px!important}.t39{border-bottom-right-radius:0!important;border-bottom-left-radius:0!important;padding:30px!important}.t77{mso-line-height-alt:20px!important;line-height:20px!important}.t64{width:380px!important}.t3{width:44px!important}.t25,.t31,.t37{width:420px!important}\r\n}\r\n</style>\r\n<!--[if !mso]>-->\r\n<link href=\"https://fonts.googleapis.com/css2?family=Albert+Sans:wght@500;800&amp;display=swap\" rel=\"stylesheet\" type=\"text/css\" />\r\n<!--<![endif]-->\r\n<!--[if mso]>\r\n<xml>\r\n<o:OfficeDocumentSettings>\r\n<o:AllowPNG/>\r\n<o:PixelsPerInch>96</o:PixelsPerInch>\r\n</o:OfficeDocumentSettings>\r\n</xml>\r\n<![endif]-->\r\n</head>\r\n<body id=\"body\" class=\"t80\" style=\"min-width:100%;Margin:0px;padding:0px;background-color:#E0E0E0;\"><div class=\"t79\" style=\"background-color:#E0E0E0;\"><table role=\"presentation\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\"><tr><td class=\"t78\" style=\"font-size:0;line-height:0;mso-line-height-rule:exactly;background-color:#E0E0E0;\" valign=\"top\" align=\"center\">\r\n<!--[if mso]>\r\n<v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"true\" stroke=\"false\">\r\n<v:fill color=\"#E0E0E0\"/>\r\n</v:background>\r\n<![endif]-->\r\n<table role=\"presentation\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" id=\"innerTable\"><tr><td align=\"center\">\r\n<table class=\"t60\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" style=\"Margin-left:auto;Margin-right:auto;\">\r\n<tr>\r\n<!--[if mso]>\r\n<td width=\"566\" class=\"t59\" style=\"width:566px;\">\r\n<![endif]-->\r\n<!--[if !mso]>-->\r\n<td class=\"t59\" style=\"width:566px;\">\r\n<!--<![endif]-->\r\n<table class=\"t58\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"width:100%;\"><tr>\r\n<td class=\"t57\" style=\"padding:50px 10px 31px 10px;\"><div class=\"t56\" style=\"width:100%;text-align:center;\"><div class=\"t55\" style=\"display:inline-block;\"><table class=\"t54\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" valign=\"top\">\r\n<tr class=\"t53\"><td></td><td class=\"t52\" width=\"546\" valign=\"top\">\r\n<table role=\"presentation\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" class=\"t51\" style=\"width:100%;\"><tr>\r\n<td class=\"t50\" style=\"background-color:transparent;\"><table role=\"presentation\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"width:100% !important;\"><tr><td align=\"center\">\r\n<table class=\"t15\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" style=\"Margin-left:auto;Margin-right:auto;\">\r\n<tr>\r\n<!--[if mso]>\r\n<td width=\"546\" class=\"t14\" style=\"width:546px;\">\r\n<![endif]-->\r\n<!--[if !mso]>-->\r\n<td class=\"t14\" style=\"width:546px;\">\r\n<!--<![endif]-->\r\n<table class=\"t13\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"width:100%;\"><tr>\r\n<td class=\"t12\"><div class=\"t11\" style=\"width:100%;text-align:center;\"><div class=\"t10\" style=\"display:inline-block;\"><table class=\"t9\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" valign=\"top\">\r\n<tr class=\"t8\"><td></td><td class=\"t7\" width=\"546\" valign=\"top\">\r\n<table role=\"presentation\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" class=\"t6\" style=\"width:100%;\"><tr>\r\n<td class=\"t5\" style=\"overflow:hidden;background-color:#586CE0;padding:49px 50px 42px 50px;border-radius:18px 18px 0 0;\"><table role=\"presentation\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"width:100% !important;\"><tr><td align=\"left\">\r\n<table class=\"t4\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" style=\"Margin-right:auto;\">\r\n<tr>\r\n<!--[if mso]>\r\n<td width=\"317\" class=\"t3\" style=\"width:317px;\">\r\n<![endif]-->\r\n<!--[if !mso]>-->\r\n<td class=\"t3\" style=\"width:317px;\">\r\n<!--<![endif]-->\r\n<table class=\"t2\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"width:100%;\"><tr>\r\n<td class=\"t1\"><div style=\"font-size:0px;\"><img class=\"t0\" style=\"display:block;border:0;height:auto;width:100%;Margin:0;max-width:100%;\" width=\"317\" height=\"91.5625\" alt=\"\" src=\"https://4b2484ea-3d45-4514-acee-bec2fce9464c.b-cdn.net/e/38957a6c-8cd4-44ac-8ec3-2f1b9b46db0f/9b37ab77-5b28-42dc-a1d9-97d4033842af.png\"/></div></td>\r\n</tr></table>\r\n</td>\r\n</tr></table>\r\n</td></tr></table></td>\r\n</tr></table>\r\n</td>\r\n<td></td></tr>\r\n</table></div></div></td>\r\n</tr></table>\r\n</td>\r\n</tr></table>\r\n</td></tr><tr><td align=\"center\">\r\n<table class=\"t49\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" style=\"Margin-left:auto;Margin-right:auto;\">\r\n<tr>\r\n<!--[if mso]>\r\n<td width=\"546\" class=\"t48\" style=\"width:546px;\">\r\n<![endif]-->\r\n<!--[if !mso]>-->\r\n<td class=\"t48\" style=\"width:546px;\">\r\n<!--<![endif]-->\r\n<table class=\"t47\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"width:100%;\"><tr>\r\n<td class=\"t46\"><div class=\"t45\" style=\"width:100%;text-align:center;\"><div class=\"t44\" style=\"display:inline-block;\"><table class=\"t43\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" valign=\"top\">\r\n<tr class=\"t42\"><td></td><td class=\"t41\" width=\"546\" valign=\"top\">\r\n<table role=\"presentation\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" class=\"t40\" style=\"width:100%;\"><tr>\r\n<td class=\"t39\" style=\"overflow:hidden;background-color:#F8F8F8;padding:40px 50px 40px 50px;border-radius:0 0 18px 18px;\"><table role=\"presentation\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"width:100% !important;\"><tr><td align=\"left\">\r\n<table class=\"t20\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" style=\"Margin-right:auto;\">\r\n<tr>\r\n<!--[if mso]>\r\n<td width=\"381\" class=\"t19\" style=\"width:381px;\">\r\n<![endif]-->\r\n<!--[if !mso]>-->\r\n<td class=\"t19\" style=\"width:381px;\">\r\n<!--<![endif]-->\r\n<table class=\"t18\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"width:100%;\"><tr>\r\n<td class=\"t17\"><h1 class=\"t16\" style=\"margin:0;Margin:0;font-family:Albert Sans,BlinkMacSystemFont,Segoe UI,Helvetica Neue,Arial,sans-serif;line-height:41px;font-weight:800;font-style:normal;font-size:30px;text-decoration:none;text-transform:none;letter-spacing:-1.56px;direction:ltr;color:#191919;text-align:left;mso-line-height-rule:exactly;mso-text-raise:3px;\">Invoice</h1></td>\r\n</tr></table>\r\n</td>\r\n</tr></table>\r\n</td></tr><tr><td><div class=\"t21\" style=\"mso-line-height-rule:exactly;mso-line-height-alt:25px;line-height:25px;font-size:1px;display:block;\">&nbsp;&nbsp;</div></td></tr><tr><td align=\"left\">\r\n<table class=\"t26\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" style=\"Margin-right:auto;\">\r\n<tr>\r\n<!--[if mso]>\r\n<td width=\"446\" class=\"t25\" style=\"width:446px;\">\r\n<![endif]-->\r\n<!--[if !mso]>-->\r\n<td class=\"t25\" style=\"width:446px;\">\r\n<!--<![endif]-->\r\n<table class=\"t24\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"width:100%;\"><tr>\r\n<td class=\"t23\"><p class=\"t22\" style=\"margin:0;Margin:0;font-family:Albert Sans,BlinkMacSystemFont,Segoe UI,Helvetica Neue,Arial,sans-serif;line-height:22px;font-weight:500;font-style:normal;font-size:14px;text-decoration:none;text-transform:none;letter-spacing:-0.56px;direction:ltr;color:#333333;text-align:left;mso-line-height-rule:exactly;mso-text-raise:2px;\">Dear customer " + customerName + "</p></td>\r\n</tr></table>\r\n</td>\r\n</tr></table>\r\n</td></tr><tr><td><div class=\"t27\" style=\"mso-line-height-rule:exactly;mso-line-height-alt:15px;line-height:15px;font-size:1px;display:block;\">&nbsp;&nbsp;</div></td></tr><tr><td align=\"left\">\r\n<table class=\"t32\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" style=\"Margin-right:auto;\">\r\n<tr>\r\n<!--[if mso]>\r\n<td width=\"446\" class=\"t31\" style=\"width:446px;\">\r\n<![endif]-->\r\n<!--[if !mso]>-->\r\n<td class=\"t31\" style=\"width:446px;\">\r\n<!--<![endif]-->\r\n<table class=\"t30\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"width:100%;\"><tr>\r\n<td class=\"t29\"><p class=\"t28\" style=\"margin:0;Margin:0;font-family:Albert Sans,BlinkMacSystemFont,Segoe UI,Helvetica Neue,Arial,sans-serif;line-height:22px;font-weight:500;font-style:normal;font-size:14px;text-decoration:none;text-transform:none;letter-spacing:-0.56px;direction:ltr;color:#333333;text-align:left;mso-line-height-rule:exactly;mso-text-raise:2px;\">Below is the invoice and payment verification photo for your order. Thank you very much for using our service.</p></td>\r\n</tr></table>\r\n</td>\r\n</tr></table>\r\n</td></tr><tr><td><div class=\"t33\" style=\"mso-line-height-rule:exactly;mso-line-height-alt:15px;line-height:15px;font-size:1px;display:block;\">&nbsp;&nbsp;</div></td></tr><tr><td align=\"left\">\r\n<table class=\"t38\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" style=\"Margin-right:auto;\">\r\n<tr>\r\n<!--[if mso]>\r\n<td width=\"446\" class=\"t37\" style=\"width:446px;\">\r\n<![endif]-->\r\n<!--[if !mso]>-->\r\n<td class=\"t37\" style=\"width:446px;\">\r\n<!--<![endif]-->\r\n<table class=\"t36\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"width:100%;\"><tr>\r\n<td class=\"t35\"><p class=\"t34\" style=\"margin:0;Margin:0;font-family:Albert Sans,BlinkMacSystemFont,Segoe UI,Helvetica Neue,Arial,sans-serif;line-height:22px;font-weight:500;font-style:normal;font-size:14px;text-decoration:none;text-transform:none;letter-spacing:-0.56px;direction:ltr;color:#333333;text-align:left;mso-line-height-rule:exactly;mso-text-raise:2px;\">If you have any questions or need further assistance, please do not hesitate to contact our support team by replying to this email or visiting our support page.</p></td>\r\n</tr></table>\r\n</td>\r\n</tr></table>\r\n</td></tr></table></td>\r\n</tr></table>\r\n</td>\r\n<td></td></tr>\r\n</table></div></div></td>\r\n</tr></table>\r\n</td>\r\n</tr></table>\r\n</td></tr></table></td>\r\n</tr></table>\r\n</td>\r\n<td></td></tr>\r\n</table></div></div></td>\r\n</tr></table>\r\n</td>\r\n</tr></table>\r\n</td></tr><tr><td align=\"center\">\r\n<table class=\"t76\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" style=\"Margin-left:auto;Margin-right:auto;\">\r\n<tr>\r\n<!--[if mso]>\r\n<td width=\"600\" class=\"t75\" style=\"width:600px;\">\r\n<![endif]-->\r\n<!--[if !mso]>-->\r\n<td class=\"t75\" style=\"width:600px;\">\r\n<!--<![endif]-->\r\n<table class=\"t74\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"width:100%;\"><tr>\r\n<td class=\"t73\"><div class=\"t72\" style=\"width:100%;text-align:center;\"><div class=\"t71\" style=\"display:inline-block;\"><table class=\"t70\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" valign=\"top\">\r\n<tr class=\"t69\"><td></td><td class=\"t68\" width=\"600\" valign=\"top\">\r\n<table role=\"presentation\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" class=\"t67\" style=\"width:100%;\"><tr>\r\n<td class=\"t66\" style=\"padding:0 50px 0 50px;\"><table role=\"presentation\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"width:100% !important;\"><tr><td align=\"center\">\r\n<table class=\"t65\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" style=\"Margin-left:auto;Margin-right:auto;\">\r\n<tr>\r\n<!--[if mso]>\r\n<td width=\"420\" class=\"t64\" style=\"width:420px;\">\r\n<![endif]-->\r\n<!--[if !mso]>-->\r\n<td class=\"t64\" style=\"width:420px;\">\r\n<!--<![endif]-->\r\n<table class=\"t63\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"width:100%;\"><tr>\r\n<td class=\"t62\"><p class=\"t61\" style=\"margin:0;Margin:0;font-family:Albert Sans,BlinkMacSystemFont,Segoe UI,Helvetica Neue,Arial,sans-serif;line-height:22px;font-weight:500;font-style:normal;font-size:12px;text-decoration:none;text-transform:none;direction:ltr;color:#888888;text-align:center;mso-line-height-rule:exactly;mso-text-raise:3px;\">© 2022 Flash Inc. All Rights Reserved<br/></p></td>\r\n</tr></table>\r\n</td>\r\n</tr></table>\r\n</td></tr></table></td>\r\n</tr></table>\r\n</td>\r\n<td></td></tr>\r\n</table></div></div></td>\r\n</tr></table>\r\n</td>\r\n</tr></table>\r\n</td></tr><tr><td><div class=\"t77\" style=\"mso-line-height-rule:exactly;mso-line-height-alt:50px;line-height:50px;font-size:1px;display:block;\">&nbsp;&nbsp;</div></td></tr></table></td></tr></table></div><div class=\"gmail-fix\" style=\"display: none; white-space: nowrap; font: 15px courier; line-height: 0;\">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</div></body>\r\n</html>",
                IsBodyHtml = true
            };

            byte[] decodedData = Convert.FromBase64String(imageURL);
            Attachment image = new Attachment(new MemoryStream(decodedData), "image.png");
            Attachment pdfFile = new Attachment(new MemoryStream(pdf), "Bill.pdf");
            mailMessage.Attachments.Add(pdfFile);
            mailMessage.Attachments.Add(image);
            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
