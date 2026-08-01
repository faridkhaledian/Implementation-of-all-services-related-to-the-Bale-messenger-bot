// BaleBotManager Dashboard — interactions

document.addEventListener('DOMContentLoaded', function () {
  initSidebarToggle();
  initComposerPreview();
  initTokenField();
  initConnectionTest();
});

/* ---- Mobile sidebar toggle ---- */
function initSidebarToggle() {
  var toggle = document.querySelector('[data-sidebar-toggle]');
  var sidebar = document.querySelector('.sidebar');
  if (!toggle || !sidebar) return;

  toggle.addEventListener('click', function () {
    sidebar.classList.toggle('open');
  });

  document.addEventListener('click', function (e) {
    if (!sidebar.classList.contains('open')) return;
    if (sidebar.contains(e.target) || toggle.contains(e.target)) return;
    sidebar.classList.remove('open');
  });
}

/* ---- Send Message page: live bubble preview + char counter ---- */
function initComposerPreview() {
  var textarea = document.querySelector('[data-message-input]');
  var bubble = document.querySelector('[data-preview-bubble]');
  var counter = document.querySelector('[data-char-counter]');
  if (!textarea || !bubble) return;

  var MAX = 4096;

  function render() {
    var value = textarea.value;
    if (value.trim().length === 0) {
      bubble.textContent = 'پیام شما اینجا نمایش داده می‌شود…';
      bubble.classList.add('empty');
    } else {
      bubble.textContent = value;
      bubble.classList.remove('empty');
    }
    if (counter) {
      counter.textContent = value.length + ' / ' + MAX;
      counter.classList.toggle('warn', value.length > MAX);
    }
  }

  textarea.addEventListener('input', render);
  render();
}

/* ---- Settings page: token visibility + copy ---- */
function initTokenField() {
  var input = document.querySelector('[data-token-input]');
  var toggleBtn = document.querySelector('[data-token-toggle]');
  var copyBtn = document.querySelector('[data-token-copy]');

  if (toggleBtn && input) {
    toggleBtn.addEventListener('click', function () {
      var isHidden = input.type === 'password';
      input.type = isHidden ? 'text' : 'password';
      toggleBtn.setAttribute('aria-label', isHidden ? 'پنهان کردن توکن' : 'نمایش توکن');
      toggleBtn.innerHTML = isHidden ? eyeOffIcon() : eyeIcon();
    });
  }

  if (copyBtn && input) {
    copyBtn.addEventListener('click', function () {
      if (!input.value) return;
      navigator.clipboard.writeText(input.value).then(function () {
        var original = copyBtn.innerHTML;
        copyBtn.innerHTML = checkIcon();
        setTimeout(function () { copyBtn.innerHTML = original; }, 1400);
      });
    });
  }
}

/* ---- Settings page: test connection ---- */
function initConnectionTest() {
  var btn = document.querySelector('[data-test-connection]');
  var panel = document.querySelector('[data-connection-panel]');
  if (!btn || !panel) return;

  btn.addEventListener('click', function () {
    var url = btn.getAttribute('data-url');
    if (!url) return;

    btn.disabled = true;
    var originalText = btn.textContent;
    btn.textContent = 'در حال بررسی…';

    fetch(url, { method: 'POST' })
      .then(function (res) { return res.json(); })
      .then(function (data) {
        updateConnectionPanel(panel, data);
      })
      .catch(function () {
        updateConnectionPanel(panel, { success: false, message: 'ارتباط با سرور برقرار نشد.' });
      })
      .finally(function () {
        btn.disabled = false;
        btn.textContent = originalText;
      });
  });
}

function updateConnectionPanel(panel, data) {
  var ring = panel.querySelector('[data-pulse-ring]');
  var label = panel.querySelector('[data-connection-label]');
  var botName = panel.querySelector('[data-bot-name]');

  if (ring) ring.classList.toggle('offline', !data.success);
  if (label) label.textContent = data.success ? 'متصل است' : (data.message || 'اتصال ناموفق بود');
  if (botName && data.botName) botName.textContent = data.botName;
}

/* ---- tiny inline icons ---- */
function eyeIcon() {
  return '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.8" width="18" height="18"><path d="M1 12s4-7 11-7 11 7 11 7-4 7-11 7-11-7-11-7Z"/><circle cx="12" cy="12" r="3"/></svg>';
}
function eyeOffIcon() {
  return '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.8" width="18" height="18"><path d="M3 3l18 18M10.6 10.6a3 3 0 0 0 4.24 4.24M9.9 5.09A10.9 10.9 0 0 1 12 5c7 0 11 7 11 7a13.2 13.2 0 0 1-3.17 3.83M6.6 6.6C3.9 8.3 2 11 2 11s4 7 11 7c1.2 0 2.3-.2 3.36-.55"/></svg>';
}
function checkIcon() {
  return '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" width="18" height="18"><path d="M20 6 9 17l-5-5"/></svg>';
}
