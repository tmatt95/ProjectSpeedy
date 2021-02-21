import React, { useState, useEffect } from 'react';
import { useParams } from "react-router-dom";
import * as bootstrap from 'bootstrap';
import { CardGrid } from '../Components/CardGrid'

export function Project({ setBreadCrumbs, breadCrumbs, globalMessage }) {
    /**
     * GET parameters.
     */
    let { projectId } = useParams();

    /**
     * Used to run code only once on page load.
     */
    const [runOnce, setRunOnce] = useState(false);
    useEffect(() => {    
        if(runOnce === false){
            document.title = `Project ${projectId}`;  
            setBreadCrumbs(breadCrumbs.concat([{ address:"test", text:"text test" }]));
            setRunOnce(true);
        }  
    }, [runOnce, setBreadCrumbs, breadCrumbs, projectId]);


    /**
     * Dummy data containing exisiting bets.
     */
    const [problems, setProblems] = useState([
        { name: "Problem 0", address: "/project/1/1" },
        { name: "Problem 1", address: "/project/2/2" },
        { name: "Problem 2", address: "/project/3/3" },
        { name: "Problem 3", address: "/project/4/4" },
        { name: "Problem 4", address: "/project/5/5" },
        { name: "Problem 5", address: "/project/6/6" }]);

    /**
     * The name of a new problem.
     */
    const [newProblemName, setNewProblemName] = useState("");

    /**
     * Create a new bet
     * @param {*} event The submit form event
     */
    const CreateNewProblem = (event) => {
        event.preventDefault();
        var myModalEl = document.getElementById('newModal')
        var modal = bootstrap.Modal.getInstance(myModalEl)
        modal.hide();
        setProblems(problems.concat({ name: `Problem ${problems.length} - ${newProblemName}`, address:"/" }))
        setNewProblemName("");
        globalMessage({ message: "Problem Added", class: "alert-success" });
    }

    return <>
        <div className="row">
            <div className="col">
                <h1>Project {projectId}</h1>
            </div>
        </div>

        <CardGrid data={problems} />

        <form onSubmit={(event) => CreateNewProblem(event)}>
            <div className="modal fade" id="newModal" tabIndex="-1" aria-labelledby="newProjectModalLabel" aria-hidden="true">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title" id="newProjectModalLabel">New Problem</h5>
                            <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                            <p>Use the form to quickly add a problem. These can be fleshed out after being created.</p>
                            <div className="mb-3">
                                <label htmlFor="exampleInputEmail1" className="form-label">Name</label>
                                <input type="text" className="form-control" value={newProblemName} onChange={(event) => setNewProblemName(event.target.value)} id="exampleInputEmail1" aria-describedby="nameHelp" />
                                <div id="nameHelp" className="form-text">The name of your problem.</div>
                            </div>
                        </div>
                        <div className="modal-footer">
                            <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                            <button type="submit" className="btn btn-primary">Add Problem</button>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </>;
}