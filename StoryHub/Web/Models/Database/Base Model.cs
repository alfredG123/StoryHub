namespace Web.Models
{
    public abstract class BaseModel
    {
        /// <summary>
        /// Return the ID of the model object
        /// </summary>
        public abstract int ID { get; set; }
    }
}
