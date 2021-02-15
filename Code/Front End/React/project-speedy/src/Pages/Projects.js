import React, { useState } from 'react';
import * as bootstrap from 'bootstrap';

export function Projects({globalMessage}) {
    const [test, setTest] = useState([{name:"Card 0"}, {name:"Card 1"}]);
    const addItem = () => setTest(test.concat({name:`Card ${test.length}`}));
    return <>
        <h1>Projects</h1>
        <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#newProjectModal">
            Launch demo modal
        </button>

        {test.map(t => <div>{t.name}</div>)}

        <div class="modal fade" id="newProjectModal" tabindex="-1" aria-labelledby="newProjectModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="newProjectModalLabel">New Project</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <p>Use the form to quickly add projects. These can be fleshed out after being created.</p>
                        <form>
                            <div class="mb-3">
                                <label for="exampleInputEmail1" class="form-label">Name</label>
                                <input type="text" class="form-control" id="exampleInputEmail1" aria-describedby="nameHelp" />
                                <div id="nameHelp" class="form-text">The name you would like to call your new project.</div>
                            </div>
                        </form>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                        <button type="button" class="btn btn-primary" onClick={() => {
                            var myModalEl = document.getElementById('newProjectModal')
                            var modal = bootstrap.Modal.getInstance(myModalEl)
                            modal.hide();
                            addItem();
                            globalMessage({message: "Item Added", class:"alert-success"});
                        }}>Add Project</button>
                    </div>
                </div>
            </div>
        </div>
    </>;
}