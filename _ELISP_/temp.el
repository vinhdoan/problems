(defun tntcheck ()
  (interactive)
  (if (bound-and-true-p erlang-mode)
      (message "ON")
    (message "OFF"))
  )

(setq auto-mode-alist (cons '("\\.tuan\\'" . tntcheck) auto-mode-alist))
