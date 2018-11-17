using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace RapperAPI.Controllers {
    public class GroupController : Controller {
        List<Group> allGroups {get; set;}
        List<Artist> allArtists {get; set;}
        public GroupController() {
            allGroups = JsonToFile<Group>.ReadJson();
            allArtists = JsonToFile<Artist>.ReadJson();
        }
        // 1.) Create a route /groups that returns all groups as JSON
        [HttpGet("groups")]
        public JsonResult Groups(){
            return Json(allGroups);
        }

        // 2.) Create a route /groups/name/{name} that returns all groups that match the provided name
        [HttpGet("groups/name/{name}")]
        public JsonResult groupName(string name) {
            var query = allGroups.Where(group => group.GroupName == name);
            return Json(query);
        }

        // 3.) Create a route /groups/id/{id} that returns all groups with the provided Id value
        [HttpGet("groups/id/{id}")]
        public JsonResult groupId(int id) {
            var query = allGroups.Where(group => group.Id == id);
            return Json(query);
        }
        
        // 4.) (Optional) Add an extra boolean parameter to the group routes called displayArtists that will include members for all Group JSON responses
        [HttpGet("groups/displayArtists/{boolean}")]
        public JsonResult displayArtists(bool boolean){
            var groupsArtists = allGroups.Join(allArtists, group => group.Id, artist => artist.GroupId, (group,artist) => {
                group.Members.Add(artist);
                return group; 
            }).Distinct().GroupBy(group => group.Id);
            return Json(groupsArtists);
        }
    }
}