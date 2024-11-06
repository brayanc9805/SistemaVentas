namespace SV.API.Utilidad
{
    public class Response<T> //<T> Para volver la clase generica y recibir cualquier objeto
    {
        public bool Status {  get; set; }
        public T Value { get; set; }
        public string Message {  get; set; }
    }
}
