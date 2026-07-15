using Microsoft.AspNetCore.Builder;
using System.Net;

namespace CLothes_DOTNET.ExtendMethods
{
    public static class AppExtends
    {
        public static void AddStatusCodePage(this IApplicationBuilder app)
        {
            app.UseStatusCodePages(apperor =>
            {
                apperor.Run(async context =>
                {
                    var respone = context.Response;
                    var code = respone.StatusCode;

                    var content = @$"<html>
					<head>
						<meta charset ='UTF-8'/>
						<title>Loi {code} - {(HttpStatusCode)code} </title>
					</head>
					<body>
						<p style ='color:red; font-size: 30px'>
							co loi xay ra: {code}
						</p>
					</body>
					</html>";
                    await respone.WriteAsync(content);
                });
            });// Code 400 -599
        }
    }
}
