import { IBet } from "../../Interfaces/IPage";

export default function BetStatus(bet: IBet)
{
    switch (bet.status)
    {
        case "Created": {
            return <div className="card">
                <div className="card-body">
                    <h2>Start Bet</h2>
                    <p>When your ready to begin work on the bet click start. The time will start counting down and the bet will become active. Good luck!</p>
                    <button className="btn btn-primary" onClick={() => { alert("test"); }}>Start bet</button>
                </div>
            </div>
        }
        case "In Progress": {
            return <>In Progress sect</>
        }
        case "Finished": {
            return <>Finished sect</>
        }
    }
}