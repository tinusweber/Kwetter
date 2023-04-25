import React, { useState } from "react";
import {
  makeStyles,
  Typography,
  Grid,
  Button,
  Paper,
  TextField,
  Box,
} from "@material-ui/core";

import ticketService from "../../services/ticket.service";
import { useEffect } from "react";
import authService from "../../services/auth.service";
import TicketTable from "../../components/tickets/ticketoverview/TicketTable";
import './supportpage.module.css';

const useStyles = makeStyles((theme) => ({
  root: {
    fontFamily: "Roboto, sans-serif",
  },
  subtitle: {
    textAlign: "center",
    fontStyle: "italic",
    fontWeight: "bold",
  },
  title: {
    fontSize: "58px",
    textAlign: "center",
    fontStyle: "italic",
    fontWeight: "bold",
  },
  paper: {
    marginLeft: "20px",
    marginRight: "20px",
    marginTop: "20px",
    minHeight: "500px",
  },
  paper2: {
    marginLeft: "20px",
    position: "sticky",
    top: 0,
    marginRight: "20px",
    marginTop: "20px",
    minHeight: "500px",
  },

  [theme.breakpoints.down("sm")]: {
    title: {
      fontSize: "30px",
    },
    paper: {
      height: "400px",
      width: "90vw",
    },
    paper2: {
      position: "sticky",
        top: 0,
    },
    position: {
      display: "block",
    },
    text: {
      marginTop: "10px",
      width: "60vw",
    },
  },

  [theme.breakpoints.up("md")]: {
    position: {
      display: "flex",
    },
    text: {
      marginTop: "10px",
    },
  },
}));
export default function SupportPage() {
  const classes = useStyles();
  const [titleValue, setTitleValue] = useState("");
  const [descValue, setDescValue] = useState("");
  const [error, setErrorMessage] = useState("");
  const [tickets, setTickets] = useState([]);

  const handleClick = (event) => {
    if (titleValue === "" || descValue === "") {
      setErrorMessage("Not all required fields were filled in!");
    } else {
      ticketService.addTicket(titleValue, descValue);
      document.location.reload();
    }
  };

  useEffect(() => {
    authService.getCurrentUser().then((result) => {
      ticketService
        .getUnawnseredTickets(result.idString)
             .then((res) => {
                 console.log(res)
                    setTickets(res);}
            );

    });
  }, []);

  return (
    <div className={classes.root}>
      <Grid>
        <Typography className={classes.title}>{"SUPPORT"}</Typography>
      </Grid>
      <Box
        direction="row"
        justify="space-evenly"
        alignItems="center"
        className={classes.position}
      >
        <Grid item xs={7}>
          <Paper className={classes.paper}>
            <Typography className={classes.subtitle}>ACTIVE TICKETS</Typography>
            <div className="ticketsList">
              <TicketTable tickets={tickets} />
            </div>
          </Paper>
        </Grid>
        <Grid item xs={4}>
          <Paper className={classes.paper2}>
            <Typography className={classes.subtitle}>
              SUPPORT TICKET
            </Typography>
            <Grid container direction="column" alignItems="center">
              <TextField
                value={titleValue}
                onChange={(e) => setTitleValue(e.target.value)}
                className={classes.text}
                label={"Question Topic"}
                variant="outlined"
                size="small"
              />
              <TextField
                value={descValue}
                onChange={(e) => setDescValue(e.target.value)}
                className={classes.text}
                label={"Description"}
                multiline
                rows={10}
                variant="outlined"
              />
              <Button onClick={handleClick}>{"Create Ticket"}</Button>
              {error ? <div className={classes.error}>{error}</div> : null}
            </Grid>
          </Paper>
          
        </Grid>
      </Box>
    </div>
  );
}
