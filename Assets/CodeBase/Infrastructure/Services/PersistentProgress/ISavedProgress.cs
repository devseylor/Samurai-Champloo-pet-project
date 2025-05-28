using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgessReader 
    {
        void LoadProgress(PlayerProgress progress);
    }

    public interface ISavedProgress : ISavedProgessReader
    {
        void UpdateProgress(PlayerProgress progress);
    }
}