using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDoDataAccess;

namespace ToDoWebApi.Controllers
{
    public class ToDoController : ApiController
    {
        public ToDoController()
        {
        }

        public IHttpActionResult Get(string userid, int todoid)
        {
            ToDo temp = null;
            using (AdvanceToDoEntities entities = new AdvanceToDoEntities())
            {
                try
                {
                    temp = entities.ToDos.Where(t => t.id == todoid).Where(t => t.userid == userid).FirstOrDefault();
                    if (temp == null)
                    {
                        return NotFound();
                    }
                    else
                    {

                        return Ok(entities.ToDos.Where(t => t.id == todoid).Where(t=>t.userid == userid).FirstOrDefault());
                    }
                }
                catch (Exception e)
                {
                    return BadRequest("Can not find ToDo");
                }
            }
        }
        public IEnumerable<ToDo> Get(string userid)
        {
            using (AdvanceToDoEntities entities = new AdvanceToDoEntities())
            {
                return entities.ToDos.Where(t => t.userid == userid).ToList();
            }
        }

        public IHttpActionResult Post([FromBody] ToDo todo)
        {
            using (AdvanceToDoEntities entities = new AdvanceToDoEntities())
            {
                try
                {
                    entities.ToDos.Add(todo);
                    entities.SaveChanges();
                    return Ok(entities.ToDos.Where(t => t.userid == todo.userid).ToList());
                }
                catch (Exception e)
                {
                    return BadRequest("Can not add the todo");
                }
            }
        }

        [HttpPut]
        public IHttpActionResult Put(string userid, [FromBody] ToDo todo)
        {
            ToDo temp = null;
            using (AdvanceToDoEntities entities = new AdvanceToDoEntities())
            {

                try
                {
                    temp = entities.ToDos.Where(t => t.userid == userid).Where(t => t.id == todo.id).FirstOrDefault();
                    if (temp == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        temp.id = temp.id;
                        temp.title = todo.title;
                        temp.description = todo.description;
                        temp.endDate = todo.endDate;
                        temp.userid = temp.userid;


                        entities.SaveChanges();
                        return Ok(entities.ToDos.Where(t => t.userid == userid).ToList());
                    }

                }
                catch (Exception e)
                {
                    return BadRequest("Can not insert ToDo");
                }
            }
        }

        public IHttpActionResult Delete(string userid,int id)
        {
            ToDo temp;
            using (AdvanceToDoEntities entities = new AdvanceToDoEntities())
            {

                try
                {
                    temp = entities.ToDos.Where(t => t.id == id).Where(t => t.userid==userid).FirstOrDefault();
                    if (temp == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        entities.ToDos.Remove(entities.ToDos.Where(t => t.id == id).Where(t => t.userid==userid).FirstOrDefault());
                        entities.SaveChanges();
                        return Ok(entities.ToDos.Where(t => t.userid == userid).ToList());
                    }

                }
                catch (Exception e)
                {
                    return BadRequest("Unable to Delete todo");
                }
            }
        }
    }
}
