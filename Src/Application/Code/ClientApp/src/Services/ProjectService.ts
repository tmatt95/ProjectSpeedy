import { CardItem } from "../Components/CardGrid";
import { IProject } from "../Interfaces/IPage";

export class ProjectService
{
    /**
     * Gets a list of projects.
     * @returns List of projects.
     */
    static async GetAll(): Promise<CardItem[]>
    {
        const response = await fetch("/api/projects/");
        const json = await response.json();
        return json.rows;
    }

    /**
     * Gets a project.
     * @returns Project info.
     */
     static async Get(projectId:string): Promise<IProject>
     {
         const response = await fetch(`/api/project/${projectId}`);
         return response.json();
    }
    
    /**
     * Adds a new project.
     * @returns Adds a new project
     */
     static async Put(data:string): Promise<Response>
     {
        return await fetch(
             "/api/project/",
             {
                method: 'PUT',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                 body: data,
              }
         );
     }
}