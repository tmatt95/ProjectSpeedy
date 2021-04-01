import { Dispatch, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { IBet, IPage } from "../Interfaces/IPage";
import { BetService } from "../Services/BetService";

export function Bet(pageProps: IPage)
{

    /**
         * GET parameters.
         */
    let { problemId, projectId, betId }: { problemId: string, projectId: string, betId: string } = useParams();

    /**
     * Page model definition.
     */
    var defaultBet: IBet = { name: "", status: "", description: "", successCriteria: "" };
    const [bet, setBet]: [IBet, Dispatch<IBet>] = useState(defaultBet);

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
            BetService.Get(projectId, problemId, betId).then(
                (data) =>
                {
                    // Sets the model against the page.
                    setBet(data);
                    //data.isLoaded = true;

                    // Sets the project name.
                    document.title = `Bet ${bet.name}`;

                    // Set the breadcrumbs.
                    pageProps.setBreadCrumbs([
                        { address: "/", text: "Projects", isLast: false },
                        { address: `/project/${projectId}`, text: "Project Name", isLast: false },
                        { address: `/project/${projectId}/${problemId}`, text: "ProblemName", isLast: true }
                    ]);
                },
                (error) =>
                {
                    alert(error);
                }
            );
        }
    }, [runOnce, pageProps, projectId, problemId, bet.name, betId]);

    function getSectionName()
    {

        if (bet.status === "Created")
        {
            return <>
                <h2><label htmlFor="name" className="form-label">Name</label></h2>
                <input type="text" className="form-control mb-3" id="name" />
            </>
        }
        return <>
            <h2>Name</h2>
            <div className="mb-3">{bet.name}</div>
        </>;
    }

    return <>
        <h1>Bet</h1>
        <p>Status: {bet.status}</p>

        {getSectionName()}

        <h2>Description</h2>
        <p>{bet.description}</p>

        <h2>Measures of Success</h2>
        <p>{bet.successCriteria}</p>

        <h2>Time Given To Bet</h2>

        <h2>Start Bet</h2>

        <nav className="mt-3">
            <div className="nav nav-tabs" id="nav-tab" role="tablist">
                <button className="nav-link active" id="nav-comments-tab" data-bs-toggle="tab" data-bs-target="#nav-comments" type="button" role="tab" aria-controls="nav-comments" aria-selected="true">Comments</button>
                <button className="nav-link" id="nav-feedback-tab" data-bs-toggle="tab" data-bs-target="#nav-feedback" type="button" role="tab" aria-controls="nav-feedback" aria-selected="false">Feedback</button>
                <button className="nav-link" id="nav-outcomes-tab" data-bs-toggle="tab" data-bs-target="#nav-outcomes" type="button" role="tab" aria-controls="nav-outcomes" aria-selected="false">Outcomes</button>
            </div>
        </nav>
        <div className="tab-content" id="nav-tabContent">
            <div className="tab-pane fade show active" id="nav-comments" role="tabpanel" aria-labelledby="nav-comments-tab">...</div>
            <div className="tab-pane fade" id="nav-feedback" role="tabpanel" aria-labelledby="nav-feedback-tab">...</div>
            <div className="tab-pane fade" id="nav-outcomes" role="tabpanel" aria-labelledby="nav-outcomes-tab">...</div>
        </div>

    </>;
}