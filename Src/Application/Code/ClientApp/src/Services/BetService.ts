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
}