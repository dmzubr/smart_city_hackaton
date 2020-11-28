using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Serilog;

using UG.Model;
using UG.ORM;

namespace UG.Utils.Cli
{
    public interface ICitiesImportService
    {
        Task ImportCities(string filePath);
    }

    public class CitiesImportServiceImpl : ICitiesImportService
    {
        private readonly IRegionService _regionService;
        private readonly ICityService _cityService;
        private readonly ICityTypeService _cityTypeService;

        public CitiesImportServiceImpl(IRegionService regionService,
            ICityService cityService,
            ICityTypeService cityTypeService)
        {
            this._regionService= regionService;
            this._cityService = cityService;
            this._cityTypeService = cityTypeService;
        }

        public async Task ImportCities(string filePath)
        {
            var lines = await File.ReadAllLinesAsync(filePath);
            var headerRowsCount = 1;
            lines = lines.Skip(headerRowsCount).ToArray();

            var citiesTypes = await this._cityTypeService.GetList();

            var regionsInitial = await this._regionService.GetList();
            var citiesInitial = await this._cityService.GetList();

            var regions = regionsInitial.ToList();
            var cities = citiesInitial.ToList();

            foreach (var line in lines)
            {
                // город	Субъект	ОКТМО	2018	размер	Численность населения	Площадь
                var spl = line.Split(new char[] { '\t' });

                var region = regions.FirstOrDefault(X => X.Name == spl[1]);
                if (region == null)
                {
                    region = new Region { Name = spl[1] };
                    await this._regionService.AddAndAssignId(region);
                    regions.Add(region);
                }

                var city = cities.FirstOrDefault(X => string.Equals(X.Name, spl[0]));
                if (city == null)
                {
                    city = new City 
                    {
                        Name = spl[0] ,
                        OKTMO = spl[2],
                        RegionId = region.RegionId,
                        CityTypeId = citiesTypes.FirstOrDefault(X => string.Equals(X.Name, spl[4])).CityTypeId
                    };
                    await this._cityService.Add(city);
                    cities.Add(city);
                }                
            }
        }        
    }    
}
