import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { State, Action, StateContext } from '@ngxs/store';

import { GraphStateModel } from './interfaces/Graph-state-model';
import { GetGraphData } from './graph.actions'; 

@State<GraphStateModel>({
    name: 'graph',
    defaults: {
        elements: [],
    }
})
@Injectable()
export class GraphState {
    elements: any = [];

    constructor(private http: HttpClient) {}

    @Action(GetGraphData)
    getGraphData(ctx: StateContext<GraphStateModel>) {
        this.http.get<any[]>("/Node").subscribe({
            next: result => {
                this.makeNodes(result);
                this.setElements(ctx)
            }, 
            error: error => console.error(error)
        });
    }

    makeNodes(nodes: any[]) {
        // push nodes
        nodes.forEach(node => {
            let newNode = {
            data: {id: node.id}
            }
            this.elements.push(newNode);
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
        this.elements.push(newEdge);
    }

    setElements(ctx: any) {
        const state = ctx.getState();
        ctx.setState({
            ...state,
            elements: this.elements
        });
    }
}