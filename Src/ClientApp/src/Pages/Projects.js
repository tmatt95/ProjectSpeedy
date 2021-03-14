import React, { useState, useEffect } from 'react';
import * as bootstrap from 'bootstrap';
import { CardGrid } from '../Components/CardGrid'

export function Projects({ setBreadCrumbs, breadCrumbs, globalMessage }) {
    /**
     * Existing projects.
     */
    const [projects, setProjects] = useState([
        { name: "Project 0", address: "/project/1" },
        { name: "Project 1", address: "/project/2" },
        { name: "Project 2", address: "/project/3" },
        { name: "Project 3", address: "/project/4" },
        { name: "Project 4", address: "/project/5" },
        { name: "Project 5", address: "/project/6" }]);

    /**
     * Add a project to the list of existing projects.
     * @param {*} name Name of the project
     * @param {*} id Id of the project
     */
    const addProject = (name, id) => setProjects(projects.concat({ name: `${name}`, address: `/project/${id}` }));

    /**
     * Name of the new project.
     */
    const [newProjectName, setNewProjectName] = useState("");

    /**
     * Used to run code only once on page load.
     */
    const [runOnce, setRunOnce] = useState(false);
    useEffect(() => {    
        if(runOnce === false){
            document.title = `Projects`;  
            setBreadCrumbs([{text: "Projects", address:"/" }]);
            setRunOnce(true);
        }  
    }, [runOnce, setBreadCrumbs, breadCrumbs]);

    /**
     * Create New Project
     * @param {*} event The submit form event
     */
    const CreateNewProject = (event) => {
        event.preventDefault();
        let myModalEl = document.getElementById('newModal')
        let modal = bootstrap.Modal.getInstance(myModalEl)
        modal.hide();
        addProject(newProjectName, projects.length +1);
        setNewProjectName("");
        globalMessage({ message: "Project added successfully", class: "alert-success" });
    }

    return <>
        <div className="row">
            <div className="col">
                <h1>Projects</h1>
            </div>
        </div>

        <CardGrid data={projects} />

        <form onSubmit={(event) => CreateNewProject(event)}>
            <div className="modal fade" id="newModal" tabIndex="-1" aria-labelledby="newProjectModalLabel" aria-hidden="true">
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
    </>;
}