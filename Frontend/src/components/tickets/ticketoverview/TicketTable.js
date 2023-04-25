import React from 'react'
import { TableContainer, Table, TableRow, TableCell, TableBody, TableHead } from '@material-ui/core'
import TicketRow from './TicketRow'

export default function TicketTable({ tickets }) {
  return (
    <div>
      <TableContainer>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>{'Title'}</TableCell>
              <TableCell>{'Description'}</TableCell>
              <TableCell>{'Issued Date'}</TableCell>
              <TableCell>{'Reply'}</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {tickets.map((ticket, idx) => (
              <TicketRow ticket={ticket} key={idx} />
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </div>
  )
}
