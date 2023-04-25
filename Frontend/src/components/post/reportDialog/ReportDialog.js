import React, { useEffect, useState } from 'react'
import Button from '@material-ui/core/Button'
import Dialog from '@material-ui/core/Dialog'
import DialogActions from '@material-ui/core/DialogActions'
import DialogContent from '@material-ui/core/DialogContent'
import DialogContentText from '@material-ui/core/DialogContentText'
import DialogTitle from '@material-ui/core/DialogTitle'
import ReportService from '../../../services/report.service'
import { useAlert } from 'react-alert'

import './ReportDialog.css'
export default function ReportDialog({ open, postId }) {
  useEffect(() => {
    setShouldOpen(open)
  }, [open])
  const [shouldOpen, setShouldOpen] = useState(false)
  const alert = useAlert()

  const reportPost = () => {
    ReportService.Add(postId, '')
      .then((result) => {
        alert.info(result)
      })
      .catch((error) => {
        alert.error(error.response.data.message)
      })
  }

  return (
    <div>
      <Dialog open={shouldOpen} aria-labelledby='alert-dialog-title' aria-describedby='alert-dialog-description'>
        <DialogTitle id='alert-dialog-title'>{'Report this post?'}</DialogTitle>
        <DialogContent>
          <DialogContentText id='alert-dialog-description'>Are you sure you want to report this post?</DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setShouldOpen(false)} color='primary'>
            No
          </Button>
          <Button
            onClick={() => {
              setShouldOpen(false)
              reportPost()
            }}
            color='primary'
            autoFocus
          >
            Yes
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  )
}
