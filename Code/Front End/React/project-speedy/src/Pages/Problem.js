import React, { useState, useEffect } from 'react';
import { useParams } from "react-router-dom";
import * as bootstrap from 'bootstrap';
import { CardGrid } from '../Components/CardGrid'

export function Problem({ setBreadCrumbs, breadCrumbs, globalMessage }) {
    /**
     * GET parameters.
     */
    let { problemId, projectId } = useParams();

    /**
     * Used to run code only once on page load.
     */
    const [runOnce, setRunOnce] = useState(false);
    useEffect(() => {    
        if(runOnce === false){
            document.title = `Problem ${problemId}`;  
            setBreadCrumbs(breadCrumbs.concat([{ address:`/`, text:"Project" }, [{ address:`/`, text:"Problem" }]]));
            setRunOnce(true);
        }  
    }, [runOnce, setBreadCrumbs, breadCrumbs, projectId]);


    /**
     * Dummy data containing exisiting bets.
     */
    const [bets, setBets] = useState([
        { name: "Bet 0", address: "/project/1" },
        { name: "Bet 1", address: "/project/2" },
        { name: "Bet 2", address: "/project/3" },
        { name: "Bet 3", address: "/project/4" },
        { name: "Bet 4", address: "/project/5" },
        { name: "Bet 5", address: "/project/6" }]);

    /**
     * The name of a new bet.
     */
    const [newBetName, setNewBetName] = useState("");

    /**
     * Create a new bet
     * @param {*} event The submit form event
     */
    const CreateNewBet = (event) => {
        event.preventDefault();
        var myModalEl = document.getElementById('newModal')
        var modal = bootstrap.Modal.getInstance(myModalEl)
        modal.hide();
        setBets(bets.concat({ name: `Problem ${bets.length} - ${newBetName}`, address:"/" }))
        setNewBetName("");
        globalMessage({ message: "Problem Added", class: "alert-success" });
    }

    return <>
        <div className="row">
            <div className="col">
                <h1>Problem {problemId}</h1>
            </div>
        </div>

        <CardGrid data={bets} />

        <form onSubmit={(event) => CreateNewBet(event)}>
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
                                <input type="text" className="form-control" value={newBetName} onChange={(event) => setNewBetName(event.target.value)} id="exampleInputEmail1" aria-describedby="nameHelp" />
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