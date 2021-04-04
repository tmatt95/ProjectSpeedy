import { IProblem } from "../Interfaces/IPage";

export class ProblemService
{
    /**
     * Gets a problem.
     * @param projectId Id of the project the problem is in.
     * @param problemId Id of the problem to load.
     * @returns information on the problem.
     */
    static async Get(projectId: string, problemId: string): Promise<IProblem>
    {
        const response = await fetch(`/api/project/${projectId}/problem/${problemId}`);
        return response.json();
    }

    /**
     * Add a new problem.
     * @param projectId Id of the project the problem will be linked to.
     * @param data Information on the new problem.
     * @returns A response object containing if the add was a success.
     */
     static async Put(projectId: string, data: string): Promise<Response>
     {
         return fetch(
             `/api/project/${projectId}/problem`,
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
     * Update a problem.
     * @param projectId Id of the project the problem will be linked to.
     * @param problemId Id of the problem to be updated.
     * @param data Information on the new problem.
     * @returns A response object containing if the add was a success.
     */
    static async Post(projectId: string, problemId: string, data: string): Promise<Response>
    {
        return fetch(
            `/api/project/${projectId}/problem/${problemId}`,
            {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: data,
            }
        );
    }
}