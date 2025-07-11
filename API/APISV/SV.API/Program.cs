using SV.IOC;

namespace SV.API
{
    public class Program
    {
        //Proyecto finalizado https://www.youtube.com/watch?v=pQsO2-thpz8&list=PLx2nia7-PgoA1-y-qrxCQii0-EmC4JZ5e&index=16

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(); 
            //Se pasa configuracion para la cadena de conexion
            builder.Services.InyectarDependencias(builder.Configuration);

            //Activar CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("NuevaPolitica", app => {
                    app.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });

            });
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

            //Se agrega politica para CORS
            app.UseCors("NuevaPolitica");


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
