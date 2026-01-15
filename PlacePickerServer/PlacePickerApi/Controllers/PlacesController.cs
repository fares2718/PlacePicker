using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PlacePickerApi.Models;
using PlacePickerApi.requests;

namespace PlacePickerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlacesController : ControllerBase
    {
        [HttpGet("places", Name = "GetAllPlaces")]
        public async Task<IActionResult> GetAllPlaces()
        {
            var PlacesJson = await System.IO.File.ReadAllTextAsync("./data/places.json");
            var PlacesData = JsonSerializer.Deserialize<List<Place>>(PlacesJson)!;

            return Ok(PlacesData);
        }

        [HttpPut("user-places", Name = "AddUserPlace")]

        public async Task<IActionResult> AddUserPlace(AddUserPlaceRequest req)
        {
            var placesJson = await System.IO.File.ReadAllTextAsync("./data/places.json");
            var placesData = JsonSerializer.Deserialize<List<Place>>(placesJson);

            var place = placesData?.FirstOrDefault(p => p.Id == req.placeId);
            if (place == null)
            {
                return NotFound();
            }

            var userPlacesJson = await System.IO.File.ReadAllTextAsync("./data/user-places.json");
            var userPlacesData = JsonSerializer.Deserialize<List<Place>>(userPlacesJson)
                                 ?? new List<Place>();

            if (!userPlacesData.Any(p => p.Id == place.Id))
            {
                userPlacesData.Add(place);
            }

            await System.IO.File.WriteAllTextAsync(
                "./data/user-places.json",
                JsonSerializer.Serialize(userPlacesData)
            );

            return Ok(new { userPlaces = userPlacesData });
        }

        [HttpGet("user-places", Name = "GetUserPlaces")]
        public async Task<IActionResult> GetUserPlaces()
        {
            var userPlacesJson = await System.IO.File.ReadAllTextAsync("./data/user-places.json");
            var userPlacesData = JsonSerializer.Deserialize<List<Place>>(userPlacesJson)!;

            return Ok(userPlacesData);
        }

        [HttpDelete("user-places/{placeId}", Name = "RemoveUserPlace")]

        public async Task<IActionResult> RemoveUserPlace(string placeId)
        {
            var userPlacesJson = await System.IO.File.ReadAllTextAsync("./data/user-places.json");
            var userPlacesData = JsonSerializer.Deserialize<List<Place>>(userPlacesJson)
                                 ?? new List<Place>();

            var placeToRemove = userPlacesData.FirstOrDefault(p => p.Id == placeId);
            if (placeToRemove == null)
            {
                return NotFound();
            }

            userPlacesData.Remove(placeToRemove);

            await System.IO.File.WriteAllTextAsync(
                "./data/user-places.json",
                JsonSerializer.Serialize(userPlacesData)
            );

            return Ok(new { userPlaces = userPlacesData });
        }

    }
}