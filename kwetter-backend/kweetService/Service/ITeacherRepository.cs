using System.Reflection;

namespace kweetService.Service
{
    public interface ITeacherRepository
    {
        List<Model.Teacher> GetAll();
        Model.Teacher Get(int id);
    }
}
