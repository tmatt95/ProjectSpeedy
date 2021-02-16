import React, { useState } from 'react';
import {useParams} from "react-router-dom";
import * as bootstrap from 'bootstrap';
import { CardGrid, Card } from '../Components/CardGrid'

export function Project({ globalMessage }) {
    let { projectId } = useParams();
    const [bets, setBets] = useState([
        { name: "Bet 0", address: "/project/1" },
        { name: "Bet 1", address: "/project/2" },
        { name: "Bet 2", address: "/project/3" },
        { name: "Bet 3", address: "/project/4" },
        { name: "Bet 4", address: "/project/5" },
        { name: "Bet 5", address: "/project/6" }]);
    const addItem = (name) => setBets(bets.concat({ name: `Bet ${bets.length} - ${name}` }));
    const [newBetName, setNewBetName] = useState("");

    /**
     * Create New Bet
     * @param {*} event The submit form event
     */
    const CreateNewBet = (event) => {
        event.preventDefault();
        var myModalEl = document.getElementById('newProjectModal')
        var modal = bootstrap.Modal.getInstance(myModalEl)
        modal.hide();
        addItem(newBetName);
        setNewBetName("");
        globalMessage({ message: "Item Added", class: "alert-success" });
    }

    return <>
        <h1>Project {projectId}</h1>
        <button type="button" className="btn btn-primary" data-bs-toggle="modal" data-bs-target="#newProjectModal">
            Add New Bet
        </button>

        <div className="row">
            <div className="col-4 p-2">
                <Card text="Add New Bet" address="/about" />
            </div>
            <CardGrid data={bets} />
        </div>

        <form onSubmit={(event) => CreateNewBet(event)}>
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
                                <input type="text" className="form-control" value={newBetName} onChange={(event) => setNewBetName(event.target.value)} id="exampleInputEmail1" aria-describedby="nameHelp" />
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