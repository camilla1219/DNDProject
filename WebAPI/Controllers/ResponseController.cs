using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var todoItems = await _context.TodoItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();

            if (todoItems == null || !todoItems.Any())
            {
                return NoContent(); // No items found, return 204 No Content
            }

            return Ok(todoItems); // Return 200 OK with the list
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound(); // Return 404 if the item is not found
            }

            return Ok(ItemToDTO(todoItem)); // Return 200 OK with the found item
        }

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItemDTO todoDTO)
        {
            if (id != todoDTO.Id)
            {
                return BadRequest("ID mismatch."); // Return 400 if the ID in the URL doesn't match the DTO
            }

            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound(); // Return 404 if the item doesn't exist
            }

            // Update the item
            todoItem.Name = todoDTO.Name;
            todoItem.IsComplete = todoDTO.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return NotFound(); // Return 404 if the item was not found during save due to a concurrency issue
            }

            return NoContent(); // Return 204 No Content on success
        }

        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> PostTodoItem(TodoItemDTO todoDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoDTO.IsComplete,
                Name = todoDTO.Name
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                ItemToDTO(todoItem)); // Return 201 Created with the new item
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound(); // Return 404 if the item doesn't exist
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent(); // Return 204 No Content on success
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
           new TodoItemDTO
           {
               Id = todoItem.Id,
               Name = todoItem.Name,
               IsComplete = todoItem.IsComplete
           };
    }
}
