import { Component, Inject, OnInit, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
declare function cytoscape(object: any): any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit, AfterViewInit {
  @ViewChild('cy') cyDiv!: ElementRef;
  graph: any;
  graphElements: any[] = [];

  /*
  [ // list of graph elements to start with
    { // node a
      data: { id: 'a' }
    },
    { // node b
      data: { id: 'b' }
    },
    { // edge ab
      data: { id: 'ab', source: 'a', target: 'b' }
    }
  ]
  */

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<any[]>("/Node").subscribe({
      next: result => {
        this.makeNodes(result);
        this.initGraph()
      }, 
      error: error => console.error(error)
    });
  }

  ngOnInit() {}

  ngAfterViewInit() {}

  makeNodes(nodes: any[]) {
    // push nodes
    nodes.forEach(node => {
      let newNode = {
        data: {id: node.id}
      }
      this.graphElements.push(newNode);

      // push edges
      node.srcEdges.forEach((edge: any) => {
        this.makeEdge(edge);
      });
    });


  }

  makeEdge(edge: any) {
    let edgeId = edge.srcID + '' + edge.destID;
    let newEdge = { // edge ab
      data: { id: edgeId, source: edge.srcID, target: edge.destID }
    }

    this.graphElements.push(newEdge);
  }

  initGraph(){
    this.graph = cytoscape({
      container: document.querySelector("#cy"), // container to render in
      elements: this.graphElements,

      style: [ // the stylesheet for the graph
        {
          selector: 'node',
          style: {
            'background-color': '#666',
            'label': 'data(id)'
          }
        },
    
        {
          selector: 'edge',
          style: {
            'width': 3,
            'line-color': '#ccc',
            'target-arrow-color': '#ccc',
            'target-arrow-shape': 'triangle',
            'curve-style': 'bezier'
          }
        }
      ],
    
      layout: {
        name: 'grid',
        rows: 1
      }
    
    });
  }
}
