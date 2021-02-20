import React, { useState } from 'react';
import * as bootstrap from 'bootstrap';
import { CardGrid } from '../Components/CardGrid'

export function Projects({ globalMessage }) {
    const [projects, setTest] = useState([
        { name: "Card 0", address: "/project/1" },
        { name: "Card 1", address: "/project/2" },
        { name: "Card 2", address: "/project/3" },
        { name: "Card 3", address: "/project/4" },
        { name: "Card 4", address: "/project/5" },
        { name: "Card 5", address: "/project/6" }]);
    const addItem = (name) => setTest(projects.concat({ name: `Card ${projects.length} - ${name}` }));
    const [newProjectName, setNewProjectName] = useState("");

    /**
     * Create New Project
     * @param {*} event The submit form event
     */
    const CreateNewProject = (event) => {
        event.preventDefault();
        var myModalEl = document.getElementById('newProjectModal')
        var modal = bootstrap.Modal.getInstance(myModalEl)
        modal.hide();
        addItem(newProjectName);
        setNewProjectName("");
        globalMessage({ message: "Item Added", class: "alert-success" });
    }

    return <>
        <div className="row">
            <div className="col">
                <h1>Projects</h1>
            </div>
        </div>

        <CardGrid data={projects} />

        <form onSubmit={(event) => CreateNewProject(event)}>
            <div className="modal fade" id="newProjectModal" tabIndex="-1" aria-labelledby="newProjectModalLabel" aria-hidden="true">
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
                                <div id="nameHelp" className="form-text">The name you would like to call your new project.</div>
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