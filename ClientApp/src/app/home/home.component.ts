import { Component, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
declare function cytoscape(object: any): any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements AfterViewInit {
  @ViewChild('cy') cyDiv!: ElementRef;
  cy: any;

  ngAfterViewInit() {
        
    this.cy = cytoscape({
      container: document.querySelector("#cy"), // container to render in
      elements: [ // list of graph elements to start with
        { // node a
          data: { id: 'a' }
        },
        { // node b
          data: { id: 'b' }
        },
        { // edge ab
          data: { id: 'ab', source: 'a', target: 'b' }
        }
      ],

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
