using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


namespace RapperAPI.Controllers {

    
    public class ArtistController : Controller {

        private List<Artist> allArtists;

        public ArtistController() {
            allArtists = JsonToFile<Artist>.ReadJson();
        }

        //This method is shown to the user navigating to the default API domain name
        //It just display some basic information on how this API functions
        [Route("")]
        [HttpGet]
        public string Index() {
            //String describing the API functionality
            string instructions = "Welcome to the Music API~~\n========================\n";
            instructions += "    Use the route /artists/ to get artist info.\n";
            instructions += "    End-points:\n";
            instructions += "       *Name/{string}\n";
            instructions += "       *RealName/{string}\n";
            instructions += "       *Hometown/{string}\n";
            instructions += "       *GroupId/{int}\n\n";
            instructions += "    Use the route /groups/ to get group info.\n";
            instructions += "    End-points:\n";
            instructions += "       *Name/{string}\n";
            instructions += "       *GroupId/{int}\n";
            instructions += "       *ListArtists=?(true/false)\n";
            return instructions;
        }
        // 1.) Create a route for /artists that returns all artists as JSON
        [HttpGet("artists")]
        public JsonResult artists() {
            return Json(allArtists);
        }
        // 2.) Create a route /artists/name/{name} that returns all artists that match the provided name(RealName)
        [HttpGet("artists/name/{name}")]
        public JsonResult artists(string name) {
            var query = allArtists.Where(artist => artist.ArtistName == name);
            return Json(query);
        }

        // 3.) Create a route /artists/realname/{name} that returns all artists who real names match the provided name
        [HttpGet("artists/realname/{name}")]
        public JsonResult realName(string name) {
            var query = allArtists.Where(artist => artist.RealName == name);
            return Json(query);
        }

        // 4.) Create a route /artists/hometown/{town} that returns all artists who are from the provided town
        [HttpGet("artists/hometown/{town}")]
        public JsonResult hometown(string town) {
            var query = allArtists.Where(artist => artist.Hometown.Contains(town));
            return Json(query);
        }

        // 5.) Create a route /artists/groupid/{id} that returns all artists who have a GroupId that matches id
        [HttpGet("artists/groupid/{id}")]
        public JsonResult groupID(int id) {
            var query = allArtists.Where(artist => artist.GroupId == id);
            return Json(query);
        }
        
    }
}