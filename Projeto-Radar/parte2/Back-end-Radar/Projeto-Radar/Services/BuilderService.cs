namespace Projeto_Radar.Services
{

    public class BuilderService<T>
    {

        public static T Builder(object objDto)
        {
            var obj = Activator.CreateInstance<T>();

            foreach (var prop in objDto.GetType().GetProperties())
            {
                var propObj = obj?.GetType().GetProperty(prop.Name);
                if (propObj != null)
                {
                    propObj.SetValue(obj, prop.GetValue(objDto));
                }
            }
            return obj;
        }

    }
}
