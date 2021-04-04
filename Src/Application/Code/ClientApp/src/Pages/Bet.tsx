import { Dispatch, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { IBet, IPage } from "../Interfaces/IPage";
import { BetService } from "../Services/BetService";
import BetTabs from "../Components/Bet/BetTabs";
import BetForm from "../Components/Bet/BetForm";
import BetStatus from "../Components/Bet/BetStatus";

export function Bet(pageProps: IPage)
{
    /**
         * GET parameters.
         */
    let { problemId, projectId, betId }: { problemId: string, projectId: string, betId: string } = useParams();

    /**
     * Page model definition.
     */
    var defaultBet: IBet = { name: "", status: "", description: "", successCriteria: "", isLoaded: false, timeCurrent: 0, timeTotal: 0 };
    const [bet, setBet]: [IBet, Dispatch<IBet>] = useState(defaultBet);

    /**
     * Used to run code only once on page load.
     */
    const [runOnce, setRunOnce] = useState(false);
    useEffect(() =>
    {
        if (runOnce === false)
        {
            // Hides any previous messages
            pageProps.globalMessageHide();

            // We only want to run this once on page load.
            setRunOnce(true);

            // Loads the projects onto the page
            BetService.Get(projectId, problemId, betId).then(
                (data) =>
                {
                    // Sets the model against the page.
                    setBet(data);
                    data.isLoaded = true;

                    // Sets the project name.
                    document.title = `Bet ${bet.name}`;

                    // Set the breadcrumbs.
                    pageProps.setBreadCrumbs([
                        { address: "/", text: "Projects", isLast: false },
                        { address: `/project/${projectId}`, text: "Project Name", isLast: false },
                        { address: `/project/${projectId}/problem/${problemId}`, text: "ProblemName", isLast: false },
                        { address: `/project/${projectId}/problem/${problemId}/bet/${betId}`, text: "BetName", isLast: true }
                    ]);
                },
                (error) =>
                {
                    alert(error);
                }
            );
        }
    }, [runOnce, pageProps, projectId, problemId, bet.name, betId]);

    if (bet.isLoaded !== true)
    {
        return <></>;
    }

    return <>
        <BetForm bet={bet} />
        <BetStatus bet={bet}/>
        <BetTabs bet={bet} />
    </>;
}