using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToDoListApi.Data;
using ToDoListApi.DTOs;
using ToDoListApi.Models;
using ToDoListApi.Services;
using ToDoListApi.Services.Interfaces;

namespace ToDoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemsController : ControllerBase
    {
        private readonly IMapper _mapper; // Inject AutoMapper instance
        private readonly ITaskItemService _taskItemService;

        public TaskItemsController(ITaskItemService taskItemService, IMapper mapper)
        {
            _mapper = mapper;
            _taskItemService = taskItemService;
        }

        // GET: api/TaskItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDTO>>> GetTaskItems()
        {
            var taskItems =  await _taskItemService.GetAllTaskItemsAsync();
            return Ok(_mapper.Map<IEnumerable<TaskItemDTO>>(taskItems));
        }

        // GET: api/TaskItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDTO>> GetTaskItem(int id)
        {
            var taskItem = await _taskItemService.GetTaskItemByIdAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<TaskItem, TaskItemDTO>(taskItem));
        }

        // PUT: api/TaskItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskItem(int id, TaskItemDTO taskItemDTO)
        {
            var taskItem = _mapper.Map<TaskItemDTO, TaskItem>(taskItemDTO);

            if (id != taskItem.Id)
            {
                return BadRequest();
            }

            await _taskItemService.UpdateTaskItemAsync(taskItem);
            return NoContent();
        }

        // POST: api/TaskItems
        [HttpPost]
        public async Task<ActionResult<TaskItemDTO>> AddTaskItem(TaskItemDTO taskItemDTO)
        {
            var taskItem = _mapper.Map<TaskItemDTO, TaskItem>(taskItemDTO);

            await _taskItemService.AddTaskItemAsync(taskItem);
            return CreatedAtAction("GetTaskItem", new { id = taskItem.Id }, _mapper.Map<TaskItem, TaskItemDTO>(taskItem));
        }

        // DELETE: api/TaskItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(int id)
        {
            await _taskItemService.DeleteTaskItemAsync(id);
            return NoContent();
        }
    }
}
