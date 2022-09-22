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
    public class EdgeController : ControllerBase
    {
        private readonly GraphApiContext _context;

        public EdgeController(GraphApiContext context)
        {
            _context = context;
        }

        // GET: api/Edge
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Edge>>> GetEdges()
        {
          if (_context.Edges == null)
          {
              return NotFound();
          }
            return await _context.Edges.ToListAsync();
        }

        // GET: api/Edge/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Edge>> GetEdge(int id)
        {
          if (_context.Edges == null)
          {
              return NotFound();
          }
            var edge = await _context.Edges.FindAsync(id);

            if (edge == null)
            {
                return NotFound();
            }

            return edge;
        }

        // PUT: api/Edge/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEdge(int id, Edge edge)
        {
            var dbEdge = await _context.Edges.FindAsync(id);
            if (dbEdge == null)
            {
                return NotFound();
            }

            dbEdge.SrcID = edge.SrcID;
            dbEdge.DestID = edge.DestID;
            dbEdge.Distance = edge.Distance;
            
            _context.Entry(dbEdge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EdgeExists(id))
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

        // POST: api/Edge
        // TODO: Edge post, check duplicates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Edge>> PostEdge(Edge edge)
        {
            if (_context.Edges == null)
            {
                return Problem("Entity set 'GraphApiContext.Edges'  is null.");
            }

            var srcNode = await _context.Nodes.FindAsync(edge.SrcID);
            var destNode = await _context.Nodes.FindAsync(edge.DestID);
            if (srcNode == null || destNode == null)
            {
                return NotFound();
            }

            edge.Src = srcNode;
            edge.Dest = destNode;

            _context.Edges.Add(edge);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEdge", new { id = edge.EdgeID }, edge);
        }

        // DELETE: api/Edge/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEdge(int id)
        {
            if (_context.Edges == null)
            {
                return NotFound();
            }
            var edge = await _context.Edges.FindAsync(id);
            if (edge == null)
            {
                return NotFound();
            }

            _context.Edges.Remove(edge);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EdgeExists(int id)
        {
            return (_context.Edges?.Any(e => e.EdgeID == id)).GetValueOrDefault();
        }
    }
}
