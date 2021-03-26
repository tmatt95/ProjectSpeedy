import { useState, useEffect, FormEvent, Dispatch, SetStateAction } from 'react';
import * as bootstrap from 'bootstrap';
import { CardGrid, CardItem } from '../Components/CardGrid'
import { IPage } from '../Interfaces/IPage';
import {ProjectService} from '../Services/ProjectService'

export function Projects(pageProps: IPage)
{
    /**
     * Existing projects.
     */
    const [projects, setProjects]: [CardItem[], Dispatch<SetStateAction<CardItem[]>>] = useState(new Array<CardItem>());

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
            document.title = 'Projects';
            // pageProps.setBreadCrumbs([{ text: "Projects", address: "/", isLast: true }]);
            setRunOnce(true);

            // Loads the projects onto the page
            ProjectService.GetAll().then(
                (data) => { setProjects(data); },
                (error) =>
                    {
                    alert(error);
                    }
            );
        }
    }, [runOnce, pageProps]);

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
            let modal: bootstrap.Modal | null = bootstrap.Modal.getInstance(myModalEl);
            modal.hide();
            setNewProjectName("");
            pageProps.globalMessage({ message: "Project added successfully", class: "alert-success" });
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