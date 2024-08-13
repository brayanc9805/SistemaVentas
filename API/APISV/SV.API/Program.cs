using SV.IOC;

namespace SV.API
{
    public class Program
    {
        //TODo empezar video 5: https://www.youtube.com/watch?v=J9XqtohVnWk&list=PLx2nia7-PgoA1-y-qrxCQii0-EmC4JZ5e&index=5

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(); 
            //Se pasa configuracion para la cadena de conexion
            builder.Services.InyectarDependencias(builder.Configuration);
            //Para agregar swagger
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            if (builder.Environment.IsDevelopment())
            {
                app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }
            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
