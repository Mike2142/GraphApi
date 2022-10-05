import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Store, Select } from '@ngxs/store';
import { GraphState } from '../graph.state'
import { GetGraphData } from '../graph.actions';

declare function cytoscape(object: any): any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  @Select(GraphState) graphState$!: Observable<string[]>;
  graph: any = undefined;

  constructor(private http: HttpClient, private store: Store) {
    this.store.dispatch(new GetGraphData());
    
    this.graphState$.subscribe({
      next: (result: any) => {
        this.renderGraph(result.elements);
      }, 
      error: error => console.error(error)
    })    
  }

  renderGraph(elements: any){
    if (!elements.length) {
      return;
    };

    // copy frozen data
    let elemCopy = JSON.parse(JSON.stringify(elements));

    if (this.graph != undefined) {
      this.graph.add(elemCopy);
      return;
    }

    this.graph = cytoscape({
      container: document.querySelector("#cy"), // container to render in
      elements: elemCopy,

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
