import { CardItem } from "../Components/CardGrid";

export class ProjectService
{
    greeting: string;

    constructor(message: string)
    {
        this.greeting = message;
    }

    static async GetAll()
    {
        const response = await fetch("/api/projects");
        const json = await response.json();
        return json.rows;
    }
}