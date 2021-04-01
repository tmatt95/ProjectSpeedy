import { IBet } from "../Interfaces/IPage";

export class BetService
{    
    /**
     * Adds a new bet to the problem.
     * @param projectId Id of the project the problem will be linked to.
     * @param problemId Id of the problem the problem will be linked to.
     * @param data Information on the new problem.
     * @returns A response object containing if the add was a success.
     */
     static async Put(projectId: string, problemId: string, data:string): Promise<Response>
     {
         return fetch(
             `/api/project/${projectId}/problem/${problemId}/bet`,
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
    
    /**
     * Gets a bet.
     * @param projectId Id of the project the problem is in.
     * @param problemId Id of the problem to load.
     * @param betId Id of the bet to load.
     * @returns information on the problem.
     */
     static async Get(projectId: string, problemId: string, betId:string): Promise<IBet>
     {
         const response = await fetch(`/api/project/${projectId}/problem/${problemId}/bet/${betId}`);
         return response.json();
     }
}