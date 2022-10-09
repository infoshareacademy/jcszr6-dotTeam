namespace PlanAndRide.BusinessLogic
{
    public class ClubService : IClubService
    {
        private readonly IRepository<Club> _repository;
        public ClubService() { }
        public ClubService(IRepository<Club> repository)
        {
            _repository = repository;
        }
        
        public async Task Add(Club club)
        {
            await _repository.Add(club);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<Club> Get(int id)
        {
            {
                try
                {
                    return await _repository.Get(id);
                }
                catch
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<Club>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task Update(int id, Club club)
        {
            try
            {
                await _repository.Update(id, club);
            }
            catch
            {
                throw;
            };
        }
        public async Task<IEnumerable<Club>> FindByName(string name)
        {
            var clubs = await _repository.GetAll();
            return clubs.Where(r => r.Name.ToLower().Contains(name.Trim().ToLower()));
        }
    }
}
