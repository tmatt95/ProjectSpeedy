import { useState, useEffect, Dispatch, MouseEvent } from 'react';
import { useParams } from "react-router-dom";
import BetNewForm from '../Components/Bet/BetNewForm';
import { CardGrid, CardItem } from '../Components/CardGrid'
import { IPage, IProblem } from '../Interfaces/IPage';
import { ProblemService } from '../Services/ProblemService';
import * as bootstrap from 'bootstrap';

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

  /**
   * Resets the form validators etc.
   */
   function ResetForm()
   {
       let form: HTMLFormElement | null = document.getElementById("new-form") as HTMLFormElement;
       if (form !== null)
       {
           form.reset();
       }
   }
  
/**
 * Loads the modal onto the screen.
 */
function DisplayModal(e: MouseEvent)
{
    e.preventDefault();
    let myModalEl: HTMLElement | null = document.getElementById('newModal');
    if (myModalEl !== null)
    {
        if (dialogOpened === false)
        {
            // Resets when dialog open
            myModalEl.addEventListener('show.bs.modal', function (event)
            {
               ResetForm();
            });
            setDialogOpened(true);
        }

        // Opens the modal.
        let modal = new bootstrap.Modal(myModalEl, { keyboard: false });
        if (modal != null)
        {
            modal.show();
        }
    }
}

    return <>
        <div className="row">
            <div className="col">
                <h1>{problem.name}</h1>

                <h2>Description</h2>

                <h2>Success Criteria</h2>

                <h2>Bets</h2>
                <CardGrid data={problem.bets} AddNewClick={(e) => { DisplayModal(e) }} />
                <BetNewForm projectId={projectId} problemId={problemId} setProblem={(data: IProblem) => { setProblem(data);}}/>
            </div>
        </div>
    </>;
}