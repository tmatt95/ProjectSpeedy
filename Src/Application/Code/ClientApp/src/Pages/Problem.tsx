import { useState, useEffect } from 'react';
import { useParams } from "react-router-dom";
import { CardGrid } from '../Components/CardGrid'
import { IPage } from '../Interfaces/IPage';

export function Problem(pageProps: IPage)
{
    /**
     * GET parameters.
     */
    let { problemId, projectId }: { problemId: string, projectId: string } = useParams();

    /**
     * Used to run code only once on page load.
     */
    const [runOnce, setRunOnce] = useState(false);
    useEffect(() =>
    {
        if (runOnce === false)
        {
            document.title = `Problem ${problemId}`;
            pageProps.setBreadCrumbs(pageProps.breadCrumbs.concat([
                { address: `/`, text: "Project", isLast: false },
                { address: `/`, text: "Problem", isLast: false }]));
            setRunOnce(true);
        }
    }, [runOnce, pageProps, projectId, problemId]);


    /**
     * Dummy data containing exisiting bets.
     */
    const bets = [{ name: "Bet 0", address: "/project/1" },
        { name: "Bet 1", address: "/project/2" },
        { name: "Bet 2", address: "/project/3" },
        { name: "Bet 3", address: "/project/4" },
        { name: "Bet 4", address: "/project/5" },
        { name: "Bet 5", address: "/project/6" }];

    return <>
        <div className="row">
            <div className="col">
                <h1>Problem {problemId}</h1>
            </div>
        </div>
        <CardGrid data={bets} AddNewClick={(e) => {}} />
    </>;
}