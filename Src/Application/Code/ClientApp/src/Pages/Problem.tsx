import { useState, useEffect, Dispatch } from 'react';
import { useParams } from "react-router-dom";
import ProblemBetNewForm from '../Components/ProblemBetNewForm';
import { CardGrid, CardItem } from '../Components/CardGrid'
import { IPage, IProblem } from '../Interfaces/IPage';
import { ProblemService } from '../Services/ProblemService';
import { PageFunctions } from './PageFunctions';
import { BetService } from '../Services/BetService';

export function Problem(pageProps: IPage)
{
    /**
     * GET parameters.
     */
    let { problemId, projectId }: { problemId: string, projectId: string } = useParams();

    /**
     * Page model definition.
     */
    var defaultProblem: IProblem = { name: "", bets: new Array<CardItem>(), isLoaded: false };
    const [problem, setProblem]: [IProblem, Dispatch<IProblem>] = useState(defaultProblem);

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
            ProblemService.Get(projectId, problemId).then(
                (data) =>
                {
                    // Sets the model against the page.
                    setProblem(data);
                    data.isLoaded = true;

                    // Sets the project name.
                    document.title = `Problem ${problem.name}`;

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
    }, [runOnce, pageProps, projectId, problemId, problem.name]);

    /**
     * Whether the dialog has even been opened.
     */
    const [dialogOpened, setDialogOpened] = useState(false);
    return <>
        <div className="row">
            <div className="col">
                <h1>{problem.name}</h1>
                <h2>Description</h2>
                <h2>Success Criteria</h2>
                <h2>Bets</h2>
                <CardGrid data={problem.bets} AddNewClick={(e) => { PageFunctions.DisplayModal(e, dialogOpened, (newValue) => { setDialogOpened(newValue) }) }} />
                <ProblemBetNewForm
                    title="Add Bet"
                    description="Use the form to quickly add new bets that you hope will help solve the problem. These can be fleshed out after being created."
                    buttonText="Add Bet"
                    saveAction={(values, setSubmitting, resetForm) =>
                    {
                        BetService.Put(projectId, problemId, JSON.stringify(values)).then(() =>
                        {
                            ProblemService.Get(projectId, problemId).then(
                                (data) =>
                                {
                                    // Sets the model against the page.
                                    setProblem(data);

                                    // Resets the add new problem form.
                                    resetForm({});

                                    // Close the dialog.
                                    PageFunctions.CloseDialog('newModal');
                                },
                                (error) =>
                                {
                                    alert(error);
                                }
                            );

                            // We have finished submitting the form.
                            setSubmitting(false);
                        });
                    }} />
            </div>
        </div>
    </>;
}