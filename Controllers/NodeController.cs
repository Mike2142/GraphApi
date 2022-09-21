using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GraphApi.Models;

namespace GraphDbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NodeController : ControllerBase
    {
        private readonly GraphApiContext _context;

        public NodeController(GraphApiContext context)
        {
            _context = context;
        }

        // GET: api/Node
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Node>>> GetNodes()
        {
          if (_context.Nodes == null)
          {
              return NotFound();
          }
            return await _context.Nodes.ToListAsync();
        }

        // GET: api/Node/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Node>> GetNode(int id)
        {
          if (_context.Nodes == null)
          {
              return NotFound();
          }
            var node = await _context.Nodes.FindAsync(id);

            if (node == null)
            {
                return NotFound();
            }

            return node;
        }

        // PUT: api/Node/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNode(int id, Node node)
        {
            var dbNode = await _context.Nodes.FindAsync(id);
            if (dbNode == null)
            {
                return NotFound();
            }

            dbNode.Name = node.Name;
            _context.Entry(dbNode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NodeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Node
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Node>> PostNode(Node node)
        {
          if (_context.Nodes == null)
          {
              return Problem("Entity set 'GraphApiContext.Nodes'  is null.");
          }
            _context.Nodes.Add(node);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNode", new { id = node.ID }, node);
        }

        // DELETE: api/Node/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNode(int id)
        {
            if (_context.Nodes == null)
            {
                return NotFound();
            }
            var node = await _context.Nodes.FindAsync(id);
            if (node == null)
            {
                return NotFound();
            }

            _context.Nodes.Remove(node);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NodeExists(int id)
        {
            return (_context.Nodes?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
