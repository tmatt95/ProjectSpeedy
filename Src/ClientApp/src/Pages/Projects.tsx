import { useState, useEffect, FormEvent, Dispatch } from 'react';
import * as bootstrap from 'bootstrap';
import { CardGrid } from '../Components/CardGrid'
import { BreadCrumbItem } from '../Components/BreadCrumbs';

export function Projects({ setBreadCrumbs, breadCrumbs, globalMessage }: { setBreadCrumbs: Dispatch<BreadCrumbItem[]>, breadCrumbs: BreadCrumbItem[], globalMessage: (alertMessage: { message: string, class: string }) => void })
{
    /**
     * Existing projects.
     */
    const [projects, setProjects] = useState([]);

    /**
     * Add a project to the list of existing projects.
     * @param {*} name Name of the project
     * @param {*} id Id of the project
     */
    const addProject = (name: string, id: number) => setProjects(projects);

    /**
     * Name of the new project.
     */
    const [newProjectName, setNewProjectName] = useState("");

    /**
     * Used to run code only once on page load.
     */
    const [runOnce, setRunOnce] = useState(false);
    useEffect(() =>
    {
        if (runOnce === false)
        {
            document.title = `Projects`;
            setBreadCrumbs([{ text: "Projects", address: "/", isLast: false }]);
            setRunOnce(true);

            // Loads the projects onto the page
            fetch("/api/projects")
                .then(res => res.json())
                .then(
                    (result) =>
                    {
                        //setIsLoaded(true);
                        setProjects(result.rows);
                    },
                    // Note: it's important to handle errors here
                    // instead of a catch() block so that we don't swallow
                    // exceptions from actual bugs in components.
                    (error) =>
                    {
                        //setIsLoaded(true);
                        //setError(error);
                    }
                );
        }
    }, [runOnce, setBreadCrumbs, breadCrumbs]);

    /**
     * Create New Project
     * @param {*} event The submit form event
     */
    const CreateNewProject = (event: FormEvent<HTMLFormElement>) =>
    {
        event.preventDefault();
        let myModalEl: HTMLElement | null = document.getElementById('newModal');

        if (myModalEl != null)
        {
            let modal: bootstrap.Modal | null = bootstrap.Modal.getInstance(myModalEl)
            modal.hide();
            addProject(newProjectName, projects.length + 1);
            setNewProjectName("");
            globalMessage({ message: "Project added successfully", class: "alert-success" });
        }
    }

    return (<>
        <div className="row">
            <div className="col">
                <h1>Projects</h1>
            </div>
        </div>

        <CardGrid data={projects} />

        <form onSubmit={(event) => CreateNewProject(event)}>
            <div className="modal fade" id="newModal" tabIndex={-1} aria-labelledby="newProjectModalLabel" aria-hidden="true">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title" id="newProjectModalLabel">New Project</h5>
                            <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                            <p>Use the form to quickly add projects. These can be fleshed out after being created.</p>
                            <div className="mb-3">
                                <label htmlFor="exampleInputEmail1" className="form-label">Name</label>
                                <input type="text" className="form-control" value={newProjectName} onChange={(event) => setNewProjectName(event.target.value)} id="exampleInputEmail1" aria-describedby="nameHelp" />
                            </div>
                        </div>
                        <div className="modal-footer">
                            <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                            <button type="submit" className="btn btn-primary">Add Project</button>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </>);
}