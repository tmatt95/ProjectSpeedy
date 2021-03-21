import { useState, useEffect, Dispatch, FormEvent } from 'react';
import { useParams } from "react-router-dom";
import * as bootstrap from 'bootstrap';
import { CardGrid, CardItem } from '../Components/CardGrid'
import { IPage, IProject } from '../Interfaces/IPage';

export function Project(pageProps: IPage)
{
    /**
     * GET parameters.
     */
    let { projectId }: { projectId: string } = useParams();

    /**
     * Page model definition.
     */
    var defaultProject: IProject = { name: "", problems: new Array<CardItem>(), isLoaded: false };
    const [project, setProject]: [IProject, Dispatch<IProject>] = useState(defaultProject);

    /**
     * Used to run code only once on page load.
     */
    const [runOnce, setRunOnce] = useState(false);
    useEffect(() =>
    {
        if (runOnce === false)
        {
            setRunOnce(true);

            // Loads the projects onto the page
            fetch(`/api/project/${projectId}`)
                .then(res => res.json())
                .then(
                    (result: IProject) =>
                    {
                        // Sets the model against the page.
                        setProject(result);
                        result.isLoaded = true;

                        // Sets the project name.
                        document.title = `Project ${project.name}`;

                        // Set the breadcrumbs.
                        pageProps.setBreadCrumbs([]);
                        pageProps.setBreadCrumbs(pageProps.breadCrumbs.concat([{ address: "/", text: "Projects", isLast: false }]));
                        pageProps.setBreadCrumbs(pageProps.breadCrumbs.concat([{ address: "", text: result.name, isLast: true }]));
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
    }, [runOnce, pageProps.setBreadCrumbs, pageProps.breadCrumbs, projectId, project.name]);

    /**
     * The name of a new problem.
     */
    const [newProblemName, setNewProblemName] = useState("");

    /**
     * Create a new bet
     * @param {*} event The submit form event
     */
    const CreateNewProblem = (event: FormEvent<HTMLFormElement>) =>
    {
        event.preventDefault();
        let myModalEl: HTMLElement | null = document.getElementById('newModal');

        if (myModalEl != null)
        {
            let modal: bootstrap.Modal | null = bootstrap.Modal.getInstance(myModalEl)
            modal.hide();
            pageProps.globalMessage({ message: "Problem Added", class: "alert-success" });
        }
    }

    // Output the page view.
    return <>
        <div className="row">
            <div className="col">
                <h1>{project.name}</h1>
                <p>Once a problem has been added we can then make bets on actions that can fix the issues.</p>
            </div>
        </div>

        <CardGrid data={project.problems} />

        <form onSubmit={(event) => CreateNewProblem(event)}>
            <div className="modal fade" id="newModal" tabIndex={-1} aria-labelledby="newProjectModalLabel" aria-hidden="true">
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