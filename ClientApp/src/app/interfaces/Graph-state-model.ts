import { Node } from "@angular/compiler";
import { Edge } from "./Edge";

export interface GraphStateModel {
    elements: (Node | Edge)[];
}