import React, { useState } from 'react';
import * as bootstrap from 'bootstrap';

export function Projects({globalMessage}) {
    const [test, setTest] = useState([{name:"Card 0"}, {name:"Card 1"}]);
    const addItem = (name) => setTest(test.concat({name:`Card ${test.length} - ${name}`}));
    const [newProjectName, setNewProjectName] = useState("");
    return <>
        <h1>Projects</h1>
        <button type="button" className="btn btn-primary" data-bs-toggle="modal" data-bs-target="#newProjectModal">
            Launch demo modal
        </button>

        {test.map((t, index) => <div key={index}>{t.name}</div>)}

        <form onSubmit={(event) => {
                            event.preventDefault();
                            var myModalEl = document.getElementById('newProjectModal')
                            var modal = bootstrap.Modal.getInstance(myModalEl)
                            modal.hide();
                            addItem(newProjectName);
                            setNewProjectName("");
                            globalMessage({message: "Item Added", class:"alert-success"});
                            return false;
                        }}>
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