import { IBet } from "../../Interfaces/IPage";

export default function BetStatus({ bet }: {bet: IBet})
{
    switch (bet.status)
    {
        case 'Created': {
            return <div className="card">
                <div className="card-body">
                    <h2>Start Bet</h2>
                    <p>When your ready to begin work on the bet click start. The time will start counting down and the bet will become active. Good luck!</p>
                    <button className="btn btn-primary" onClick={() => { alert("test"); }}>Start bet</button>
                </div>
            </div>
        }
        case 'In Progress': {
            return <div className="card">
                <div className="card-body">
                    <h2>Time Left</h2>
                    <button className="btn btn-primary" onClick={() => { alert("test"); }}>Finish Bet Early</button>
                </div>
            </div>
        }
        case 'Finished': {
            return <div className="card">
                <div className="card-body">
                    <h2>Record Results</h2>
                    <p>What will you do next?</p>
                    <button className="btn btn-primary" onClick={() => { alert("test"); }}>Save Result</button>
                </div>
            </div>
        }
    }
    return <></>
}