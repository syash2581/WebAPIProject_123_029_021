using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDoDataAccess;

namespace ToDoWebApi.Controllers
{
    public class UserController : ApiController
    {

        public UserController()
        {
        }
        
        public IHttpActionResult Get(string id)
        {
            User temp = null;
            using (AdvanceToDoEntities entities = new AdvanceToDoEntities())
            {
                try
                {
                    temp = entities.Users.FirstOrDefault(u => u.userid == id);
                    if (temp == null)
                    {
                        return NotFound();
                    }
                    else
                    {

                        return Ok(entities.Users.Where(u => u.userid == id).FirstOrDefault());
                    }

                }
                catch (Exception e)
                {
                    return BadRequest("Can not find User");
                }
            }
        }
        public IEnumerable<User> Get()
        {
            using (AdvanceToDoEntities entities = new AdvanceToDoEntities())
            {
                return entities.Users.ToList();
            }
        }

        [HttpPut]
        public IHttpActionResult Put(string id, [FromBody] User user)
        {
            User temp = null;
            using (AdvanceToDoEntities entities = new AdvanceToDoEntities())
            {

                try
                {
                    temp = entities.Users.FirstOrDefault(u => u.userid == id);
                    if (temp == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        temp.password = temp.password;
                        temp.email = user.email;
                        temp.contact = user.contact;
                        temp.dob = user.dob;
                        temp.name = user.name;
                        temp.userid = temp.userid;

                        entities.SaveChanges();
                        return Ok(entities.Users.ToList());
                    }

                }
                catch (Exception e)
                {
                    return BadRequest("Can not update User");
                }
            }
        }

        public IHttpActionResult Delete(string id)
        {
            User temp;
            using (AdvanceToDoEntities entities = new AdvanceToDoEntities())
            {

                try
                {
                    temp = entities.Users.FirstOrDefault(u => u.userid == id);
                    if (temp == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        entities.Users.Remove(entities.Users.FirstOrDefault(u => u.userid == id));
                        entities.SaveChanges();
                        return Ok(entities.Users.ToList());
                    }

                }
                catch (Exception e)
                {
                    return BadRequest("Unable to Delete user");
                }
            }
        }
         
        public IHttpActionResult Get(string id,string password)
        {
            User temp;
            using (AdvanceToDoEntities entities = new AdvanceToDoEntities())
            {
                try
                {
                    temp = entities.Users.FirstOrDefault(u => u.userid == id);
                    if (temp == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        if (temp.userid.Equals(id) && temp.password.Equals(password))
                        {
                            return Ok("Login Successful");
                        }
                        else
                        {
                            return BadRequest("Login Failed");
                        }
                        
                    }
                }


                catch (Exception e)
                {
                    return BadRequest("Login Failed");
                }
            }
        }


        public IHttpActionResult Post([FromBody] User user)
        {
            using (AdvanceToDoEntities entities = new AdvanceToDoEntities())
            {
                try
                {
                    if (entities.Users.FirstOrDefault(u => u.userid == user.userid) == null)
                    {
                        entities.Users.Add(user);
                        entities.SaveChanges();
                        return Ok(entities.Users.ToList());
                    }
                    else
                    {
                        return BadRequest("Can not insert the User");
                    }

                }
                catch (Exception e)
                {
                    return BadRequest("Can not insert the User");
                }
            }
        }
    }
}
